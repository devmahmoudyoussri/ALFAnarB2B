using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace fahim
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<fahimIdentityServerModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
