﻿Imports System.ComponentModel
Imports PropertyChanged

Namespace Models.OrderManagement

    <ImplementPropertyChanged>
    Public Class InventoryClient
        <DisplayName("产品编号")>
        Public Property ProductId As Integer

        <DisplayName("数量")>
        Public Property Quantity As Nullable(Of Integer)
    End Class
End Namespace