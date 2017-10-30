Imports System.Diagnostics.CodeAnalysis
Imports GalaSoft.MvvmLight.Ioc
Imports Microsoft.Practices.ServiceLocation
Imports OrderManagement.WpfClient.Service
Imports OrderManagement.WpfClient.ViewModel.Master
Imports OrderManagement.WpfClient.ViewModel.Order

Namespace ViewModel.Base
    Public Class ViewModelLocator

#Region "Constructor"

        Shared Sub New()

            ServiceLocator.SetLocatorProvider(Function() SimpleIoc.[Default])

            SimpleIoc.Default.Register(Of IOrderServiceAgent, OrderServiceAgent)()
            SimpleIoc.Default.Register(Of ICustomerServiceAgent, CustomerServiceAgent)()

            'Navigation Service
            Dim frameNavigationService As New FrameNavigationService

            frameNavigationService.Configure("OrderList", New Uri("../Views/Order/ListOrderView.xaml", UriKind.Relative))
            frameNavigationService.Configure("OrderDetail",
                                             New Uri("../Views/Order/AddOrderView.xaml", UriKind.Relative))
            SimpleIoc.Default.Register(Of IFrameNavigationService)(Function() frameNavigationService)

            SimpleIoc.Default.Register(Of MainWindowViewModel)()
            SimpleIoc.Default.Register(Of ListOrderViewModel)()
            SimpleIoc.Default.Register(Of AddOrderViewModel)()
            SimpleIoc.Default.Register(Of CustomerViewModel)()
        End Sub

#End Region

#Region "MainWindow"

        ''' <summary>
        '''     Gets the MainWindowViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property MainWindowViewModel As MainWindowViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of MainWindowViewModel)()
            End Get
        End Property

#End Region

#Region "订单"

        ''' <summary>
        '''     Gets the ListOrderViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property ListOrderViewModel As ListOrderViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of ListOrderViewModel)()
            End Get
        End Property

        ''' <summary>
        '''     Gets the AddOrderViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property AddOrderViewModel As AddOrderViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of AddOrderViewModel)()
            End Get
        End Property

#End Region

#Region "用户"

        ''' <summary>
        '''     Gets the CustomerViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property CustomerViewModel As CustomerViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of CustomerViewModel)()
            End Get
        End Property

#End Region

#Region "Methods"

        Public Shared Sub CleanUp()
        End Sub

#End Region
    End Class
End Namespace