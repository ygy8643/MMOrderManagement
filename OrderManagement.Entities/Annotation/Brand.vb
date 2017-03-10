Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(BrandHelper))>
Partial Public Class Brand
End Class

Public Class BrandHelper
    <Display(Name:="品牌编号")>
    Public Property BrandId As Integer
    <Display(Name:="品牌名称")>
    Public Property BrandName As String
    <Display(Name:="品牌日文名称")>
    Public Property BrandNameJp As String
End Class
