using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using fahim.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.CmsKit.EntityFrameworkCore;

namespace fahim.EntityFrameworkCore
{
    [DependsOn(
        typeof(fahimDomainModule),
        typeof(AbpIdentityProEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),
        typeof(LanguageManagementEntityFrameworkCoreModule),
        typeof(SaasEntityFrameworkCoreModule),
        typeof(TextTemplateManagementEntityFrameworkCoreModule),
        typeof(CmsKitProEntityFrameworkCoreModule),
        typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
        )]
    public class fahimEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            fahimEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<fahimDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

                options.AddRepository<Contract, Contracts.EfCoreContractRepository>();

            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also fahimDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}