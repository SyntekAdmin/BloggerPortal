using F21.BLOGGER.Web.Models;
using F21.BLOGGER.Model;
using F21.BLOGGER.Business;
using Stis.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;

namespace F21.BLOGGER.Web.Common
{
    public class AuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private System.Web.Configuration.AuthenticationMode _authMode;
        public AuthenticationAttribute()
        {
            _authMode = this.AuthenticationMode();
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (_authMode == System.Web.Configuration.AuthenticationMode.Forms)
                FormAuthentication(filterContext);
            else
                WindowsAuthentication(filterContext);
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                if (_authMode == System.Web.Configuration.AuthenticationMode.Windows)
                    filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        private void WindowsAuthentication(AuthenticationContext ctx)
        {
            if (ctx.HttpContext.User == null || !ctx.HttpContext.User.Identity.IsAuthenticated)
                return;

            if (ctx.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName] == null)
            {
                string domainNUserId = ctx.HttpContext.User.Identity.Name;
                string userId = domainNUserId;
                if (userId.IsNotEmptyOrWhiteSpace())
                {
                    SetTicket(x =>
                    {
                        HttpContext.Current.User = new CustomPrincipal(FormsAuthentication.FormsCookieName, x);
                    }, userId);
                    //ctx.Controller.TempData["IsAuthenticated"] = true;
                }
            }
        }

        private void FormAuthentication(AuthenticationContext ctx)
        {
            return;
        }

        private void SetTicket(Action<UserIdentity> action, string userId)
        {
            //CommonBiz biz = new CommonBiz();
            //var identity = biz.GetUserInfoByUserId(userId);
            //if (identity != null)
            //{
            //    action(identity);
            //    identity.SetTicket();
            //}
        }
    }
}