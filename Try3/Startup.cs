using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Try3.Startup))]
namespace Try3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
