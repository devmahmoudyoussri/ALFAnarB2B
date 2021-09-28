using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace fahim
{
    [Dependency(ReplaceServices = true)]
    public class fahimBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "fahim";
    }
}
