using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gartenkraft_Admin.Startup))]
namespace Gartenkraft_Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
