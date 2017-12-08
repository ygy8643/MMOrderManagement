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

        ''' <summary>
        ''' 订单状态
        ''' </summary>
        ''' <returns></returns>
        Public shared Property OrderDetailStatusList As New List(Of  ValueNamePair ) From {
            New ValueNamePair() With {.Value = OrderDetailStatus.Confirmed, .DisplayName="确定"},
            New ValueNamePair() With {.Value = OrderDetailStatus.Buyed, .DisplayName="已下单"},
            New ValueNamePair() With {.Value = OrderDetailStatus.Received, .DisplayName="已收货"},
            New ValueNamePair() With {.Value = OrderDetailStatus.Sent, .DisplayName="已发送"},
            New ValueNamePair() With {.Value = OrderDetailStatus.Complete, .DisplayName="完成"}
            }

    End Class
End Namespace