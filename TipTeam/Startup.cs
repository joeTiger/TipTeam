using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TipTeam.Startup))]
namespace TipTeam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
