using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Capstone_ECommerce_progject.Startup))]
namespace Capstone_ECommerce_progject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
