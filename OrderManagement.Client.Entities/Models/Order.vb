Imports System.ComponentModel
Imports PropertyChanged

Namespace Models
    <ImplementPropertyChanged>
    Public Class OrderClient
        Public Sub New()
            Me.CustomerClient = New CustomerClient()
            Me.OrderDetailClients = New List(Of OrderDetailClient)
        End Sub

        <DisplayName("订单号码")>
        Public Property OrderId As Integer

        <DisplayName("客户编号")>
        Public Property CustomerId As Integer

        <DisplayName("订单日期")>
        Public Property OrderDate As Nullable(Of Date)

        <DisplayName(“发货日期”)>
        Public Property ShipDate As Nullable(Of Date)

        <DisplayName(“种类”)>
        Public Property OrderType As Integer

        <DisplayName(“邮单编号”)>
        Public Property InvoiceNo As String

        <DisplayName(“运费”)>
        Public Property Freight As Nullable(Of Decimal)

        'Navigation Properties
        Public Property CustomerClient As CustomerClient
        Public Property OrderDetailClients As ICollection(Of OrderDetailClient)
    End Class
End Namespace