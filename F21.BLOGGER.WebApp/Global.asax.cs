#region Using

using F21.BLOGGER.Model;
using F21.BLOGGER.WebApp.Models;
using Newtonsoft.Json;
using Stis.Web.Mvc;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using NLog;
using NLog.Web;

#endregion

namespace F21.BLOGGER.WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
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

                try
                {
                    var identity = JsonConvert.DeserializeObject<InfUser>(authTicket.UserData);
                    //CustomPrincipal user = new CustomPrincipal(authTicket.Name, identity);
                    CustomPrincipal user = new CustomPrincipal(identity.USER_NAME, identity);

                    HttpContext.Current.User = user;
                }
                catch (Exception ex)
                {
                    FormsAuthentication.SignOut();

                    Response.Redirect("/account/login");
                }
            }
        }

        protected void Appication_Error()
        {
            //Exception lastException = Server.GetLastError();
            //NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            //logger.Fatal(lastException);
        }
    }
}