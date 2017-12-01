Imports System.Web.Optimization

Public Module BundleConfig
    Public Sub RegisterBundles(bundles As BundleCollection)
        bundles.Add(
            New ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(
            New ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"))

        bundles.Add(
            New ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"))

        bundles.Add(
            New StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"))
    End Sub
End Module
