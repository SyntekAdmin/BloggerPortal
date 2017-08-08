using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Stis.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.Configuration;
using F21.BLOGGER.WebApp.Models;
using F21.BLOGGER.Model;

namespace F21.BLOGGER.WebApp.Common
{
    public static class SiteExtensions
    {
        public static AuthenticationMode AuthenticationMode(this object obj)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            SystemWebSectionGroup configSection = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
            AuthenticationSection auth = configSection.Authentication;

            return auth.Mode;
        }

        public static void SetTicket(this InfUser identity)
        {
            string userData = JsonConvert.SerializeObject(identity);
            //string userData = identity.USER_SEQ.ToString()  + "^" + identity.LAST_NM 
            //    + "^" + identity.FIRST_NM  +"^" + identity.USER_NAME + "^" + identity.ROLE + "^" + identity.USER_EMAIL;

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     identity.USER_SEQ.ToString(),
                     DateTime.Now,
                     DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

        public static InfUser UserIdentity(this HtmlHelper helper)
        {
            IPrincipal user = helper.ViewContext.HttpContext.User;

            if (user != null && user.Identity.IsAuthenticated)
            {
                InfUser convertIdentity = user as InfUser;

                return convertIdentity;
            }

            return null;
        }

        public static InfUser UserIdentity(this IPrincipal user)
        {
            if (user != null && user.Identity.IsAuthenticated)
                return user as InfUser;

            return null;
        }

        public static string GoBack(this HttpRequestBase request)
        {
            string url = "/";
            if (request.Cookies[Settings.Instance.ListReferrer] != null)
                url = request.Cookies[Settings.Instance.ListReferrer].Value;

            return string.Format("location.href='{0}'", url);
        }
    }
}
