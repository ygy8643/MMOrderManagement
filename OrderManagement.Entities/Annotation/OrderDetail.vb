Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(OrderDetailHelper))>
Partial Public Class OrderDetail
End Class

Public Class OrderDetailHelper
    <Display(Name:="订单明细编号")>
    Public Property OrderDetailId As Integer
    <Display(Name:="订单编号")>
    Public Property OrderId As Integer
    <Display(Name:="产品编号")>
    Public Property ProductId As Integer
    <Display(Name:="数量")>
    Public Property Quantity As Nullable(Of Integer)
    <Display(Name:="采购价格")>
    Public Property PurchasePrice As Nullable(Of Decimal)
    <Display(Name:="售出价格")>
    Public Property SoldPrice As Nullable(Of Decimal)
    <Display(Name:="订单状态")>
    Public Property Status As OrderStatus
    <Display(Name:="采购链接")>
    Public Property Link As String
End Class