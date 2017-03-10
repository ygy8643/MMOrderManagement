Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(CustomerHelper))>
Partial Public Class Customer
End Class

Public Class CustomerHelper
    <Display(Name:="客户编号")>
    Public Property CustomerId As Integer
    <Display(Name:="客户姓名")>
    Public Property Name As String
    <Display(Name:="收货地址")>
    Public Property Address As String
    <Display(Name:="邮编")>
    Public Property PostCode As String
    <Display(Name:="电话号码")>
    Public Property Phone As String
End Class