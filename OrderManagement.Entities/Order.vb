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

Partial Public Class Order
    Public Property OrderId As Integer
    Public Property CustomerId As Integer
    Public Property OrderDate As Nullable(Of Date)
    Public Property ShipDate As Nullable(Of Date)

    Public Overridable Property Customer As Customer
    Public Overridable Property OrderDetails As ICollection(Of OrderDetail) = New HashSet(Of OrderDetail)

End Class
