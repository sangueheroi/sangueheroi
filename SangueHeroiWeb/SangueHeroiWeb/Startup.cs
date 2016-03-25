using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SangueHeroiWeb.Startup))]
namespace SangueHeroiWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
