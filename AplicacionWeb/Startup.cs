using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplicacionWeb.Startup))]
namespace AplicacionWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
