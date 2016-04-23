using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(iot_pubService.Startup))]

namespace iot_pubService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}