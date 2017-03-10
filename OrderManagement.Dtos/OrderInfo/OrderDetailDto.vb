Public Class OrderDetailDto

    Public Property OrderDetailId As Integer
    Public Property OrderId As Integer
    Public Property ProductId As Integer
    Public Property Quantity As Nullable(Of Integer)
    Public Property PurchasePrice As Nullable(Of Decimal)
    Public Property SoldPrice As Nullable(Of Decimal)
    Public Property Link As String

End Class
