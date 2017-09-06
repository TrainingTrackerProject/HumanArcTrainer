using System.Web.Optimization;

namespace HumanArc.Compliance.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.maskedinput.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/ng-file-upload.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/angular-ui-date.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/font-awesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/boostrap-theme.css",
                      "~/Content/site.css",
                        "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/toastr.css"));

            var angularBundle = new ScriptBundle("~/Scripts/angularComponents")
                .IncludeDirectory("~/app", "*.js", false)
                .IncludeDirectory("~/app/shared", "*.js", true)
                .IncludeDirectory("~/app/components", "*.js", true);
            angularBundle.Transforms.Clear();
            bundles.Add(angularBundle);
        }
    }
}