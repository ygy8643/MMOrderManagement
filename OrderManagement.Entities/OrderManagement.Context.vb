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

Partial Public Class OrderManagementEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=OrderManagementEntities")
        'MyBase.Configuration.ProxyCreationEnabled = False
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Overridable Property Brands() As DbSet(Of Brand)
    Public Overridable Property Customers() As DbSet(Of Customer)
    Public Overridable Property OrderDetails() As DbSet(Of OrderDetail)
    Public Overridable Property Orders() As DbSet(Of Order)
    Public Overridable Property Products() As DbSet(Of Product)
    Public Overridable Property Inventories() As DbSet(Of Inventory)
    Public Overridable Property Species() As DbSet(Of Species)

End Class
