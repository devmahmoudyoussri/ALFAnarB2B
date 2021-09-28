using fahim.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace fahim.Web.Public.Pages
{
    /* Inherit your Page Model classes from this class.
     */
    public abstract class fahimPublicPageModel : AbpPageModel
    {
        protected fahimPublicPageModel()
        {
            LocalizationResourceType = typeof(fahimResource);
        }
    }
}
