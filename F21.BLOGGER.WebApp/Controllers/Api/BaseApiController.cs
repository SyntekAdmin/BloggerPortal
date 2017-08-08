#region Using

using System;
using F21.BLOGGER.Model;
using System.Web;
using System.Web.Http;
using System.Threading;

#endregion

namespace F21.BLOGGER.WebApp.Controllers.Api
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        public BaseApiController()
        {
            var request = HttpContext.Current.Request;
            if (request != null && request.RawUrl != null && !request.RawUrl.Contains("/account/Logout", StringComparison.OrdinalIgnoreCase))
            {
                if (Identity != null)
                {
                    var response = HttpContext.Current.Response;
                }
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

        public Int64 userSeq
        {
            get
            {
                if (Identity != null)
                {
                    return Identity.USER_SEQ;
                }
                return -1;
            }
        }
    }
}
