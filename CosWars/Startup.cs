using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CosWars.Startup))]
namespace CosWars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
