using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace fahim.Web.Public
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<fahimWebPublicModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
