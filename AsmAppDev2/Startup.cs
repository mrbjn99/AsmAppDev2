using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsmAppDev2.Startup))]
namespace AsmAppDev2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
