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

Partial Public Class OrderDetail
    Public Property OrderDetailId As Integer
    Public Property OrderId As Integer
    Public Property ProductId As Integer
    Public Property Quantity As Nullable(Of Integer)
    Public Property PurchasePrice As Nullable(Of Decimal)
    Public Property SoldPrice As Nullable(Of Decimal)
    Public Property Status As Integer
    Public Property Link As String

    Public Overridable Property Order As Order
    Public Overridable Property Product As Product

End Class
