Imports System.Diagnostics.CodeAnalysis
Imports GalaSoft.MvvmLight.Ioc
Imports GalaSoft.MvvmLight.Views
Imports Microsoft.Practices.ServiceLocation
Imports OrderManagement.WpfClient.Service
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

            frameNavigationService.Configure("OrderList", New Uri("../Views/Order/OrderListView.xaml", UriKind.Relative))
            frameNavigationService.Configure("OrderDetail",
                                             New Uri("../Views/Order/OrderDetailView.xaml", UriKind.Relative))
            SimpleIoc.Default.Register(Of IFrameNavigationService)(Function() frameNavigationService)

            SimpleIoc.Default.Register(Of MainWindowViewModel)()
            SimpleIoc.Default.Register(Of OrderListViewModel)()
            SimpleIoc.Default.Register(Of OrderDetailViewModel)()
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
        '''     Gets the OrderListViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property OrderListViewModel As OrderListViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of OrderListViewModel)()
            End Get
        End Property

        ''' <summary>
        '''     Gets the OrderDetailViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property OrderDetailViewModel As OrderDetailViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of OrderDetailViewModel)()
            End Get
        End Property

#End Region

#Region "Methods"

        Public Shared Sub CleanUp()
        End Sub

#End Region
    End Class
End Namespace