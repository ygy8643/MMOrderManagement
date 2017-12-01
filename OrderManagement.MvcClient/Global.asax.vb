Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing
Imports AutoMapper
Imports OrderManagement.MvcClient.Config

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        'Initialize Automapper
        Mapper.Initialize(Sub(c) c.AddProfile(Of MappingProfile)())

    End Sub
End Class
