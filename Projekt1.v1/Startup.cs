using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projekt1.v1.Startup))]
namespace Projekt1.v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
