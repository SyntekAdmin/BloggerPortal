﻿#region Using

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace F21.BLOGGER.WebApp
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();

            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }).RouteHandler = new DashRouteHandler();
        }
    }
}