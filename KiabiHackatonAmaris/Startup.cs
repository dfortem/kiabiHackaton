using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KiabiHackatonAmaris.Startup))]
namespace KiabiHackatonAmaris
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
