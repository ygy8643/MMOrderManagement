Imports AutoMapper
Imports OrderManagement.WpfClient.Config

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Protected Overrides Sub OnStartup(e As StartupEventArgs)

        Mapper.Initialize(Sub(c) c.AddProfile(Of MappingProfile)())

        MyBase.OnStartup(e)
    End Sub
End Class
