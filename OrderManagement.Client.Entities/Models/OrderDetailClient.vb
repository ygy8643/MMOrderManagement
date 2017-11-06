Imports System.ComponentModel
Imports PropertyChanged

Namespace Models

    <ImplementPropertyChanged>
    Public Class OrderDetailClient
        Implements ICloneable

        <DisplayName("订单明细编号")>
        Public Property OrderDetailId As Integer

        <DisplayName("订单编号")>
        Public Property OrderId As Integer

        <DisplayName("产品编号")>
        Public Property ProductId As Integer

        <DisplayName("数量")>
        Public Property Quantity As Nullable(Of Integer)

        <DisplayName("采购价格")>
        Public Property PurchasePrice As Nullable(Of Decimal)

        <DisplayName("售出价格")>
        Public Property SoldPrice As Nullable(Of Decimal)

        <DisplayName("采购链接")>
        Public Property Link As String

        Public Overridable Property ProductClient As ProductClient

        Public Function Clone() As Object Implements ICloneable.Clone
            Dim newOrderDetailClient = CType(Me.MemberwiseClone, OrderDetailClient)
            Return newOrderDetailClient
        End Function
    End Class
End Namespace