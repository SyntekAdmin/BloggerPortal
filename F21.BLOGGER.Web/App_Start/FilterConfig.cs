using F21.BLOGGER.Web.Common;
using System.Web;
using System.Web.Mvc;

namespace F21.BLOGGER.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new AuthenticationAttribute());
        }
    }
}
