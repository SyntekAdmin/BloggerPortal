using Newtonsoft.Json;
using Stis.Web.Mvc;
using Stis.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using F21.BLOGGER.Model;
using System.Threading;
using F21.BLOGGER.Web.Models;
using Stis.Core;

using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Web.Configuration;
using System.Configuration;

namespace F21.BLOGGER.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new CustomViewEngine());
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var identity = JsonConvert.DeserializeObject<UserIdentity>(authTicket.UserData);
                CustomPrincipal user = new CustomPrincipal(authTicket.Name, identity);

                HttpContext.Current.User = user;
            }
        }
    }
}
