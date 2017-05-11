Imports GalaSoft.MvvmLight.CommandWpf
Imports OrderManagement.Client.Entities
Imports OrderManagement.WpfClient.Service

Namespace ViewModel

    Public Class OrderListViewModel
        Inherits MyViewModelBase

#Region "Fields"

        ''' <summary>
        ''' 订单service
        ''' </summary>
        Private ReadOnly _orderService As IOrderService

#End Region
#Region "Properties"

        ''' <summary>
        ''' Title
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Title As String
            Get
                Return "订单管理"
            End Get
        End Property

        ''' <summary>
        ''' 订单信息
        ''' </summary>
        Private _orders As IEnumerable(Of OrderClient)
        Public Property Orders() As IEnumerable(Of OrderClient)
            Get
                Return _orders
            End Get
            Set(ByVal value As IEnumerable(Of OrderClient))
                [Set]("Orders", _orders, value)
            End Set
        End Property

        ''' <summary>
        ''' 选择的订单
        ''' </summary>
        Private _selectedOrder As OrderClient

        Public Property SelectedOrder() As OrderClient
            Get
                Return _selectedOrder
            End Get
            Set(ByVal value As OrderClient)
                [Set]("SelectedOrder", _selectedOrder, value)
            End Set
        End Property

#End Region

#Region "Commands"

        ''' <summary>
        ''' 查询订单命令
        ''' </summary>
        ''' <returns></returns>
        Public Property SearchOrderCommand As RelayCommand

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New(orderService As IOrderService)

            _orderService = orderService

            With "Commands"

                SearchOrderCommand = New RelayCommand(AddressOf SearchOrder)

            End With
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' 查询订单
        ''' </summary>
        Private Sub SearchOrder()

            Orders = _orderService.GetOrders

        End Sub

#End Region

    End Class
End Namespace
