using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F21.BLOGGER.WebApp.Startup))]

namespace F21.BLOGGER.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
