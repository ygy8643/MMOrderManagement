'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Product
    Public Property ProductId As Integer
    Public Property BrandId As Integer
    Public Property ProductName As String
    Public Property ProductNameJp As String

    Public Overridable Property Brand As Brand
    Public Overridable Property OrderDetails As ICollection(Of OrderDetail) = New HashSet(Of OrderDetail)

End Class
