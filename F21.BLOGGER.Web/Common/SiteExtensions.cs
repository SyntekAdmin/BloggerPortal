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
using F21.BLOGGER.Model;
using F21.BLOGGER.Web.Models;

namespace F21.BLOGGER.Web.Common
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

        public static void SetTicket(this UserIdentity identity)
        {
            string userData = JsonConvert.SerializeObject(identity);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                    identity.USER_SEQ,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(faCookie);
        }

        public static UserIdentity UserIdentity(this HtmlHelper helper)
        {
            IPrincipal user = helper.ViewContext.HttpContext.User;

            if (user != null && user.Identity.IsAuthenticated)
            {
                UserIdentity convertIdentity = user as UserIdentity;

                return convertIdentity;
            }

            return null;
        }

        public static UserIdentity UserIdentity(this IPrincipal user)
        {
            if (user != null && user.Identity.IsAuthenticated)
                return user as UserIdentity;

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
