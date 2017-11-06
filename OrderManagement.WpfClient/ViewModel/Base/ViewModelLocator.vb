Imports System.Diagnostics.CodeAnalysis
Imports GalaSoft.MvvmLight.Ioc
Imports MahApps.Metro.Controls.Dialogs
Imports Microsoft.Practices.ServiceLocation
Imports OrderManagement.WpfClient.Service
Imports OrderManagement.WpfClient.Service.Interfaces
Imports OrderManagement.WpfClient.ViewModel.Master
Imports OrderManagement.WpfClient.ViewModel.Order

Namespace ViewModel.Base
    Public Class ViewModelLocator

#Region "Constructor"

        Shared Sub New()

            ServiceLocator.SetLocatorProvider(Function() SimpleIoc.[Default])

            SimpleIoc.Default.Register(Of IDialogCoordinator, DialogCoordinator)()

            SimpleIoc.Default.Register(Of IListServiceAgent, ListServiceAgent)()

            SimpleIoc.Default.Register(Of IOrderServiceAgent, OrderServiceAgent)()
            SimpleIoc.Default.Register(Of ICustomerServiceAgent, CustomerServiceAgent)()
            SimpleIoc.Default.Register(Of IProductServiceAgent, ProductServiceAgent)()
            SimpleIoc.Default.Register(Of ISpeciesServiceAgent, SpeciesServiceAgent)()
            SimpleIoc.Default.Register(Of IBrandServiceAgent, BrandServiceAgent)()

            'Navigation Service
            Dim frameNavigationService As New FrameNavigationService

            frameNavigationService.Configure("OrderList", New Uri("../Views/Order/ListOrderView.xaml", UriKind.Relative))
            frameNavigationService.Configure("OrderDetail",
                                             New Uri("../Views/Order/AddOrUpdateOrderView.xaml", UriKind.Relative))
            SimpleIoc.Default.Register(Of IFrameNavigationService)(Function() frameNavigationService)

            'View Model of List
            SimpleIoc.Default.Register(Of ConstantListsViewModel)()

            'View Models of View
            SimpleIoc.Default.Register(Of MainWindowViewModel)()
            SimpleIoc.Default.Register(Of ListOrderViewModel)()
            SimpleIoc.Default.Register(Of AddOrUpdateOrderViewModel)()
            SimpleIoc.Default.Register(Of CustomerViewModel)()
            SimpleIoc.Default.Register(Of ProductViewModel)()
            SimpleIoc.Default.Register(Of SpeciesViewModel)()
            SimpleIoc.Default.Register(Of BrandViewModel)()
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

#Region "列表"

        ''' <summary>
        '''     Gets the ConstantListsViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property ConstantListsViewModel As ConstantListsViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of ConstantListsViewModel)()
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
        '''     Gets the AddOrUpdateOrderViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property AddOrUpdateOrderViewModel As AddOrUpdateOrderViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of AddOrUpdateOrderViewModel)()
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

#Region "产品"

        ''' <summary>
        '''     Gets the ProductViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property ProductViewModel As ProductViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of ProductViewModel)()
            End Get
        End Property

#End Region

#Region "种类"

        ''' <summary>
        '''     Gets the SpeciesViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property SpeciesViewModel As SpeciesViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of SpeciesViewModel)()
            End Get
        End Property

#End Region

#Region "品牌"

        ''' <summary>
        '''     Gets the BrandViewModel property.
        ''' </summary>
        <SuppressMessage _
            ("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
             Justification:="This non-static member is needed for data binding purposes.")>
        Public ReadOnly Property BrandViewModel As BrandViewModel
            Get
                Return ServiceLocator.Current.GetInstance(Of BrandViewModel)()
            End Get
        End Property

#End Region

#Region "Methods"

        Public Shared Sub CleanUp()
        End Sub

#End Region
    End Class
End Namespace