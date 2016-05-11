using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iot_pub_website.Startup))]
namespace iot_pub_website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
