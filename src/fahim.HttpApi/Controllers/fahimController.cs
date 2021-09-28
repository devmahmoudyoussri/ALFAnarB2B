using fahim.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace fahim.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class fahimController : AbpController
    {
        protected fahimController()
        {
            LocalizationResource = typeof(fahimResource);
        }
    }
}