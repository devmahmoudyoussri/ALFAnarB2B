using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using fahim.Localization;
using fahim.MultiTenancy;
using fahim.Web.Public.Menus;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Commercial;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Owl.reCAPTCHA;
using Volo.CmsKit.Pro.Public.Web;

namespace fahim.Web.Public
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(fahimHttpApiModule),
        typeof(fahimHttpApiClientModule),
        typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
        typeof(AbpAspNetCoreMvcClientModule),
        typeof(AbpHttpClientIdentityModelWebModule),
        typeof(CmsKitProPublicWebModule),
        typeof(AbpAspNetCoreMvcUiLeptonThemeModule)
        )]
    public class fahimWebPublicModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(fahimResource),
                    typeof(fahimDomainSharedModule).Assembly,
                    typeof(fahimApplicationContractsModule).Assembly,
                    typeof(fahimWebPublicModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureCache(configuration);
            ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureMultiTenancy();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureNavigationServices(configuration);
            ConfigureLeptonTheme();
            ConfigureBackgroundJobs();

            context.Services.AddreCAPTCHAV3(o =>
            {
                o.SiteKey = "test";
                o.SiteSecret = "test";
            });
        }

        private void ConfigureBackgroundJobs()
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }

        private void ConfigureLeptonTheme()
        {
            Configure<LeptonThemeOptions>(options =>
            {
                options.IsPublicWebsite = true;
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "fahim:";
            });
        }

        private void ConfigureMultiTenancy()
        {
            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies", options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(365);
                })
                .AddAbpOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);;
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                    options.ClientId = configuration["AuthServer:ClientId"];
                    options.ClientSecret = configuration["AuthServer:ClientSecret"];

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("role");
                    options.Scope.Add("email");
                    options.Scope.Add("phone");
                    options.Scope.Add("fahim");
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<fahimWebPublicModule>();
            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<fahimWebPublicModule>();

                if (hostingEnvironment.IsDevelopment())
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<fahimDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}fahim.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<fahimApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}fahim.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<fahimHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}fahim.HttpApi", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<fahimWebPublicModule>(hostingEnvironment.ContentRootPath);
                }
            });
        }

        private void ConfigureNavigationServices(IConfiguration configuration)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new fahimPublicMenuContributor(configuration));
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new fahimToolbarContributor());
            });
        }

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "fahim-Protection-Keys");
            }
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
