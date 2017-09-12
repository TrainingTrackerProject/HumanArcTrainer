﻿using System.Web;
using System.Web.Optimization;

namespace HumanArcCompliance
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/maskedinput.min.js",
                        "~/Scripts/angular.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/dataTables.colReorder.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Scripts/font-awesome.min.css",
                      "~/Content/layout.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/dataTables/css").Include(
                      "~/Scripts/jquery.dataTables.min.css",
                      "~/Scripts/bootstrap-flex.min.css",
                      "~/Scripts/bootstrap-grid.min.js",
                      "~/Scripts/dataTables.bootstrap.min.css",
                      "~/Scripts/colReorder.bootstrap.min.css"                   
                      ));
        }
    }
}
