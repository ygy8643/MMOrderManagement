Namespace Define
    Public Class ConstantLists

        ''' <summary>
        ''' 订单类型
        ''' </summary>
        ''' <returns></returns>
        Public Shared Property OrderTypeList As New List(Of ValueNamePair) From {
            New ValueNamePair() With {.Value = 0, .DisplayName = "普通订单"},
            New ValueNamePair() With {.Value = 1, .DisplayName = "拼单"}
            }
    End Class
End Namespace