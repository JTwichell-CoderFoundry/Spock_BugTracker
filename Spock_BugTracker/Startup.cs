using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spock_BugTracker.Startup))]
namespace Spock_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
