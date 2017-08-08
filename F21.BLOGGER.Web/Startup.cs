using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F21.BLOGGER.Web.Startup))]

namespace F21.BLOGGER.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
