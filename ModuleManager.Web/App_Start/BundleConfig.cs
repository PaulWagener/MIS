using System.Web.Optimization;

namespace ModuleManager.Web {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new ScriptBundle("~/bundles/scripts/main").Include(
                        "~/Scripts/angular.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/select2.min.js",
                        "~/Scripts/app/app.js").IncludeDirectory("~/Scripts/app/controllers", "*.js")); //Our angular app

            bundles.Add(new StyleBundle("~/bundles/css/main").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                      "~/Scripts/modalform.js"));
        }
    }
}