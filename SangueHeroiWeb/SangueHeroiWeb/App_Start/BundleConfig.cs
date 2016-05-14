using System.Web;
using System.Web.Optimization;

namespace SangueHeroiWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                     "~/Scripts/notify.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        //"~/Content/bootstrap-theme.css",
                        //"~/Content/bootstrap-theme.css.map",
                        //"~/Content/bootstrap-theme.min.css",
                        //"~/Content/bootstrap-theme.min.css.map",
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap.css.map",
                        //"~/Content/bootstrap.min.css",
                        //"~/Content/bootstrap.min.css.map",
                        "~/Content/font-awesome.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/controllers").Include(
                       "~/Scripts/login/login.js"));
        }
    }
}
