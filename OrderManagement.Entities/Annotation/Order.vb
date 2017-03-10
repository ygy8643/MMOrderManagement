Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(OrderHelper))>
Partial Public Class Order
End Class

Public Class OrderHelper
    <Display(Name:="订单号码")>
    Public Property OrderId As Integer
    <Display(Name:="客户编号")>
    Public Property CustomerId As Integer
    <Display(Name:="订单日期")>
    Public Property OrderDate As Nullable(Of Date)
    <Display(Name:=“发货日期”)>
    Public Property ShipDate As Nullable(Of Date)
End Class