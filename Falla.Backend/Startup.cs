using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Falla.Backend.Startup))]
namespace Falla.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
