using fahim.Localization;
using Volo.Abp.Application.Services;

namespace fahim
{
    /* Inherit your application services from this class.
     */
    public abstract class fahimAppService : ApplicationService
    {
        protected fahimAppService()
        {
            LocalizationResource = typeof(fahimResource);
        }
    }
}
