Imports OrderManagement.Client.Entities

Public Class OrderListViewModel
    Inherits MyViewModelBase

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

#Region "查询"

#End Region

End Class
