Namespace Dto

    Public Class CustomerDto

        Public Property CustomerId As Integer
        Public Property Name As String
        Public Property Address As String
        Public Property PostCode As String
        Public Property Phone As String
        Public Property WechatName As String
        Public Property TaobaoName As String

        Public Overridable Property OrderDtoes As ICollection(Of OrderDto) = New HashSet(Of OrderDto)
    End Class

End Namespace
