﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class Entities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=Entities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Overridable Property Brands() As DbSet(Of Brand)
    Public Overridable Property Customers() As DbSet(Of Customer)
    Public Overridable Property OrderDetails() As DbSet(Of OrderDetail)
    Public Overridable Property Orders() As DbSet(Of Order)
    Public Overridable Property Products() As DbSet(Of Product)

End Class
