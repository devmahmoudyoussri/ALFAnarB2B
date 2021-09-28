using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace fahim.Web.Public.Pages
{
    public class IndexModel : fahimPublicPageModel
    {
        public void OnGet()
        {

        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}
