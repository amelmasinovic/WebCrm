using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCrm.Startup))]
namespace WebCrm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
