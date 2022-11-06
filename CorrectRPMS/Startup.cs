using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CorrectRPMS.Startup))]
namespace CorrectRPMS
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
