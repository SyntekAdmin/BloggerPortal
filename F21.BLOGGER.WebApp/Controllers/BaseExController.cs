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
using System.Web.Security;
using Newtonsoft.Json;
using System.Configuration;
using F21.BLOGGER.WebApp.Common;
using F21.BLOGGER.Model;
using Stis.Web.Mvc.Attributes;


namespace F21.BLOGGER.WebApp.Controllers
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
                && !request.RawUrl.Contains("/account/Logout", StringComparison.OrdinalIgnoreCase))
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


        public InfUser Identity
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null)
                    return System.Web.HttpContext.Current.User as InfUser;
                return null;
            }
        }

        public bool CreateFolderForUpload(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }


    }
}