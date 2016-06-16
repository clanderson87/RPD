using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPD.Startup))]
namespace RPD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
