using fahim.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace fahim
{
    [DependsOn(
        typeof(fahimEntityFrameworkCoreTestModule)
        )]
    public class fahimDomainTestModule : AbpModule
    {

    }
}