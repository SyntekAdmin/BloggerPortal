#region Using

using System.Web.Mvc;

#endregion

namespace F21.BLOGGER.WebApp
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}