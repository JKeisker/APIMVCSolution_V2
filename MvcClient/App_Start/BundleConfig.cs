﻿using System.Web;
using System.Web.Optimization;

namespace MvcClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            BundleTable.Bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://code.jquery.com/jquery-3.3.1.js")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables", "https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js")
                .Include("~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/datatablescss", "https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css")
                .Include("~/Content/DataTables/css/jquery.dataTables.css"));
        }
    }
}
