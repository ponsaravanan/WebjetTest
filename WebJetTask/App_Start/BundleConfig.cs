using System.Web;
using System.Web.Optimization;

namespace MvcApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/movies").Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/movies/moviesHome.js",
                            "~/Scripts/movies/App.js",
                            "~/Scripts/movies/Controller.js",
                            "~/Scripts/movies/Service.js"

                         ));

        }
    }
}