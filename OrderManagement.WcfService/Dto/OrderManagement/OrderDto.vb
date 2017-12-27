Namespace Dto.OrderManagement

    Public Class OrderDto

        Public Property OrderId As Integer
        Public Property CustomerId As Integer
        Public Property OrderDate As Nullable(Of Date)
        Public Property ShipDate As Nullable(Of Date)
        Public Property OrderType As Integer
        Public Property InvoiceNo As String
        Public Property Freight As Nullable(Of Decimal)

        'Navigation Properties
        Public Property CustomerDto As CustomerDto
        Public Property OrderDetailDtoes As ICollection(Of OrderDetailDto)
    End Class
End Namespace
