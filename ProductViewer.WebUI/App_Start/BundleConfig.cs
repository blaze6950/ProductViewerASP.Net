using System.Web.Optimization;

namespace ProductViewer.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/sitecss").Include(
                "~/Content/scss/site.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include("~/Scripts/materializejs/source/materialize.min.js"));
        }
    }
}
