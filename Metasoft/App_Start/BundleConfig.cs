using System.Web;
using System.Web.Optimization;

namespace Metasoft
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respon.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                     "~/Content/site.min.css",
                      "~/Content/bootstrap-datepicker2.css"));
           
            bundles.Add(new ScriptBundle("~/bundles/tools").Include(
                      "~/Scripts/solid.min.js",
                     "~/Scripts/TweenMax.min.js",
                     "~/Scripts/TweenMain.js",
                      "~/Scripts/datatables.min.js",
                     "~/Scripts/bootbox.min.js",
                     "~/Scripts/metasoft.min.js",
                     "~/Scripts/jquery.mask.plugin.min.js",
                     "~/Scripts/jquery.maskMoney.js"));

            bundles.Add(new ScriptBundle("~/bundles/validations_pt-br")
                  .Include(
                      "~/Scripts/jquery-validate-custom-pt-br.js"));


        }
    }
}
