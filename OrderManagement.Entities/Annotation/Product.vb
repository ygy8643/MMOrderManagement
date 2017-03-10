Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(ProductHelper))>
Partial Public Class Product
End Class

Public Class ProductHelper
    <Display(Name:="产品编号")>
    Public Property ProductId As Integer

    <Display(Name:="产品中文名称")>
    Public Property ProductName As String

    <Display(Name:="产品日文名称")>
    Public Property ProductNameJp As String
End Class
