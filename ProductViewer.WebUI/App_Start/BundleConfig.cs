using System.Web.Optimization;

namespace ProductViewer.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/sitecss").Include(
                "~/Content/scss/site.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                "~/Scripts/materializejs/source/materialize.min.js"));

            //Styles KendoUI
            bundles.Add(new StyleBundle("~/bundles/kendo.common.material").Include(
                "~/Content/kendo/2018.3.911/kendo.common-material.min.css"));
            bundles.Add(new StyleBundle("~/bundles/kendo.mobile").Include(
                "~/Content/kendo/2018.3.911/kendo.mobile.all.min.css"));
            bundles.Add(new StyleBundle("~/bundles/kendo.material").Include(
                "~/Content/kendo/2018.3.911/kendo.material.min.css"));
            //Scripts KendoUI
            bundles.Add(new ScriptBundle("~/bundles/jszip").Include(
                "~/Scripts/kendo/2018.3.911/jszip.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendojs.all").Include(
                "~/Scripts/kendo/2018.3.911/kendo.all.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendojs.aspnetmvc").Include(
                "~/Scripts/kendo/2018.3.911/kendo.aspnetmvc.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendojs.modernizr.custom").Include(
                "~/Scripts/kendo.modernizr.custom.js"));
        }
    }
}
