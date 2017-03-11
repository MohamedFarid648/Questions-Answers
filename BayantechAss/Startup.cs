using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BayantechAss.Startup))]
namespace BayantechAss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
