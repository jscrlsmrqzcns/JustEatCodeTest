using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace JustEatCodeTestWeb.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/lib/jquery-1.9.1.min.js",
                "~/Scripts/lib/angular.js",
                "~/Scripts/lib/angular-animate.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/Services/RestaurantSearchService.js",
                "~/Scripts/app/Controllers/RestaurantSearchController.js",
                "~/Scripts/app/references.js",
                "~/Scripts/app/app.js"
                ));
        }
    }
}