Imports System.Web.Http
Imports AutoMapper

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        Mapper.Initialize(Sub(cfg) cfg.AddProfile(New MappingProfile))
    End Sub
End Class
