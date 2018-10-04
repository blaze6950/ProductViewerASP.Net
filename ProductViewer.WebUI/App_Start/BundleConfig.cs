using System.Web.Optimization;

namespace ProductViewer.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/materializejs/source/materialize.min.js",
                "~/Scripts/kendo/2018.3.911/jszip.min.js",
                "~/Scripts/kendo/2018.3.911/kendo.all.min.js",
                "~/Scripts/kendo/2018.3.911/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.modernizr.custom.js"));

            
            bundles.Add(new StyleBundle("~/bundles/sitecss").Include(
                "~/Content/kendo/2018.3.911/kendo.common-material.min.css",
                "~/Content/kendo/2018.3.911/kendo.mobile.all.min.css",
                "~/Content/kendo/2018.3.911/kendo.material.min.css",
                "~/Content/scss/site.min.css"));
        }
    }
}
