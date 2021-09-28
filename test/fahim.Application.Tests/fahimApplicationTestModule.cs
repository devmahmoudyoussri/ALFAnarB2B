using Volo.Abp.Modularity;

namespace fahim
{
    [DependsOn(
        typeof(fahimApplicationModule),
        typeof(fahimDomainTestModule)
        )]
    public class fahimApplicationTestModule : AbpModule
    {

    }
}