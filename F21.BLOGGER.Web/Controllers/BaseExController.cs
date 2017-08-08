using Stis.Core;
using Stis.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Security.Principal;
using System.Web.Routing;
using Stis.Web.Security;
using System.Web.Security;
using Newtonsoft.Json;
using System.Configuration;
using F21.BLOGGER.Model;

namespace F21.BLOGGER.Web.Controllers
{
    [Authorize]
    [CustomHandleError]
    public class BaseExController : Controller
    {
        public BaseExController()
        {
            var request = System.Web.HttpContext.Current.Request;
            if (request != null && request.RawUrl != null
                //&& !request.RawUrl.Contains("/common/PolicyCheck", StringComparison.OrdinalIgnoreCase)
                && !request.RawUrl.Contains("/Account/Logoff", StringComparison.OrdinalIgnoreCase))
            {
                if (Identity != null)
                {
                    var response = System.Web.HttpContext.Current.Response;
                    /*
                    if (response != null)
                        response.Redirect("/Common/PolicyCheck");
                    */
                }
            }
        }


        protected Dictionary<string, List<string>> CustomHeader
        {
            get
            {
                Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
                dic.Add("SessionId", new List<string>() { HttpContext.Session.SessionID });

                return dic;
            }
        }


        public UserIdentity Identity
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null)
                    return System.Web.HttpContext.Current.User as UserIdentity;

                return null;
            }
        }

    }
}