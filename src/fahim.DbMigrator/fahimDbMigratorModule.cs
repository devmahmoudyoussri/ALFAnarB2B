using fahim.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace fahim.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(fahimEntityFrameworkCoreModule),
        typeof(fahimApplicationContractsModule)
    )]
    public class fahimDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }
}
