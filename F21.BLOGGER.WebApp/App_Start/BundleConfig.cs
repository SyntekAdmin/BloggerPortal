#region Using

using System.Web.Optimization;

#endregion

namespace F21.BLOGGER.WebApp
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Script
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
                ));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
               "~/Scripts/angular.min.js",
               "~/Scripts/angular-route.js"
               ));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    "~/Scripts/bootstrap/bootstrap.min.js"
            //    ));
            bundles.Add(new ScriptBundle("~/bundles/angular-ui").Include(
                "~/Scripts/angular-ui/ui-bootstrap*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
               "~/Scripts/app/forever21App.js"
               ));

            BundleTable.EnableOptimizations = true;
        }
    }
}