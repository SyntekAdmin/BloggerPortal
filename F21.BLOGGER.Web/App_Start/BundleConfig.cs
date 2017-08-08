using System.Web;
using System.Web.Optimization;

namespace F21.BLOGGER.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Style
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/site.css",
                "~/Content/f21.css",
                "~/Content/angular-block-ui.min.css"
                ));

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
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/angular-ui").Include(
                "~/Scripts/angular-ui/ui-bootstrap*"
                ));
            
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/site.js"
                ));
            //,
            //   "~/Scripts/angular-route.min.js",
            //   "~/Scripts/angular-sanitize.min.js",
            //   "~/Scripts/angular-ui.min.js",
            //   "~/Scripts/angular-ui/ui-bootstrap.min.js",
            //   "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
            //   "~/Scripts/angular-ui.min.js",
            //   "~/Scripts/angular-block-ui.js"


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
               "~/Scripts/app/forever21App.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/app/grid").Include(
                "~/Scripts/app/grid.js"
                ));
        }
    }
}