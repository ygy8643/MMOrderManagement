Imports GalaSoft.MvvmLight.CommandWpf
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.Service
Imports OrderManagement.WpfClient.Service.Interfaces
Imports OrderManagement.WpfClient.ViewModel.Base

Namespace ViewModel.Order
    Public Class AddOrUpdateOrderViewModel
        Inherits MyViewModelBase

#Region "Fields"

        ''' <summary>
        '''     订单service
        ''' </summary>
        Private ReadOnly _orderServiceAgent As IOrderServiceAgent

#End Region

#Region "Properties"

        ''' <summary>
        '''     标题
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Title As String
            Get
                Return "订单编辑"
            End Get
        End Property

        ''' <summary>
        '''     订单信息
        ''' </summary>
        Private _order As OrderClient

        Public Property Order As OrderClient
            Get
                Return _order
            End Get
            Set
                [Set]("Order", _order, Value)
            End Set
        End Property

        ''' <summary>
        '''     选择的订单明细
        ''' </summary>
        Private _selectedOrderDetail As OrderDetailClient

        Public Property SelectedOrderDetail As OrderDetailClient
            Get
                Return _selectedOrderDetail
            End Get
            Set
                [Set]("SelectedOrderDetail", _selectedOrderDetail, Value)

                If Value Is Nothing Then
                    EditingOrderDetail = New OrderDetailClient()
                Else
                    EditingOrderDetail = Value.Clone
                End If
            End Set
        End Property

        ''' <summary>
        '''     编辑中的订单明细
        ''' </summary>
        Private _editingOrderDetail As OrderDetailClient

        Public Property EditingOrderDetail As OrderDetailClient
            Get
                Return _editingOrderDetail
            End Get
            Set
                [Set]("EditingOrderDetail", _editingOrderDetail, Value)
            End Set
        End Property

#End Region

#Region "Commands"

        ''' <summary>
        '''     查询订单命令
        ''' </summary>
        ''' <returns></returns>
        Public Property SearchOrderCommand As RelayCommand

        ''' <summary>
        '''     添加或者更新订单信息
        ''' </summary>
        ''' <returns></returns>
        Public Property AddOrUpdateOrderCommand As RelayCommand

        ''' <summary>
        '''     删除订单信息
        ''' </summary>
        ''' <returns></returns>
        Public Property DeleteOrderCommand As RelayCommand

        ''' <summary>
        '''     删除订单明细信息
        ''' </summary>
        ''' <returns></returns>
        Public Property DeleteOrderDetailCommand As RelayCommand

#End Region

#Region "Constructors"

        ''' <summary>
        '''     Constructor
        ''' </summary>
        Public Sub New(orderServiceAgent As IOrderServiceAgent)

            _orderServiceAgent = orderServiceAgent

            With "Commands"

                SearchOrderCommand = New RelayCommand(AddressOf SearchOrder)

                AddOrUpdateOrderCommand = New RelayCommand(AddressOf AddOrUpdateOrder)

                DeleteOrderCommand = New RelayCommand(AddressOf DeleteOrder)

                DeleteOrderDetailCommand = New RelayCommand(AddressOf DeleteOrderDetail)

            End With

            Order = New OrderClient()
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        '''     订单检索
        ''' </summary>
        Private Sub SearchOrder()
            If Order.OrderId <> 0 Then
                Dim result = _orderServiceAgent.GetOrder(Order.OrderId)

                If result Is Nothing Then
                    Order = New OrderClient()
                Else
                    Order = result
                End If
            End If
        End Sub

        ''' <summary>
        '''     保存订单信息
        ''' </summary>
        Private Sub AddOrUpdateOrder()
            Throw New NotImplementedException
        End Sub

        ''' <summary>
        '''     删除订单明细
        ''' </summary>
        Private Sub DeleteOrderDetail()
            Throw New NotImplementedException
        End Sub

        ''' <summary>
        '''     删除订单
        ''' </summary>
        Private Sub DeleteOrder()
            Throw New NotImplementedException
        End Sub

#End Region
    End Class
End Namespace
