Imports GalaSoft.MvvmLight.Ioc
Imports Microsoft.Practices.ServiceLocation
Imports OrderManagement.WpfClient.Service

Namespace ViewModel.Base

    Public Class ViewModelLocator

#Region "Constructor"

        Shared Sub New()

            ServiceLocator.SetLocatorProvider(Function() SimpleIoc.[Default])

            SimpleIoc.Default.Register(Of IOrderService, OrderService)()

            SimpleIoc.Default.Register(Of MainWindowViewModel)()
            SimpleIoc.Default.Register(Of OrderListViewModel)()

        End Sub

#End Region

#Region "MainWindow"

        ''' <summary>
        ''' Gets the MainWindowViewModel property.
        ''' </summary>
        <System.Diagnostics.CodeAnalysis.SuppressMessage _
                ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
                 Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property MainWindowViewModel() As MainWindowViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of MainWindowViewModel)()
            End Get
        End Property

#End Region

#Region "订单"

        ''' <summary>
        ''' Gets the MainWindowViewModel property.
        ''' </summary>
        <System.Diagnostics.CodeAnalysis.SuppressMessage _
                ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
                 Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property OrderListViewModel() As OrderListViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of OrderListViewModel)()
            End Get
        End Property

#End Region

#Region "Methods"

        Public Shared Sub CleanUp()
        End Sub

#End Region

    End Class
End Namespace