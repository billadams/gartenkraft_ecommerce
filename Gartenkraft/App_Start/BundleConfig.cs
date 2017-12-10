using System.Web;
using System.Web.Optimization;

namespace Gartenkraft {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/toTop.js",
                "~/Scripts/edit.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/bootstrap/theme.min.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Content/css").Include(
                "~/Areas/Admin/Content/bootstrap-theme.css",
                "~/Areas/Admin/Content/bootstrap-theme.min.css",
                "~/Areas/Admin/Content/bootstrap.css",
                "~/Areas/Admin/Content/bootstrap.min.css",
                "~/Areas/Admin/Content/dashboard.css",
                "~/Areas/Admin/Content/sidebar.css",
                "~/Areas/Admin/Content/Site.css",
                "~/Areas/Admin/Content/table.css"));
        }
    }
}
