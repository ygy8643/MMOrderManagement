Imports OrderManagement.Entities

Namespace Dto

    Public Class OrderDto

        Public Property OrderId As Integer
        Public Property CustomerId As Integer
        Public Property OrderDate As Nullable(Of Date)
        Public Property ShipDate As Nullable(Of Date)
        Public Property OrderType As Integer
        Public Property InvoiceNo As String

        'Navigation Properties
        Public Property CustomerDto As CustomerDto
        Public Property OrderDetailDtoes As ICollection(Of OrderDetailDto)
    End Class
End Namespace
