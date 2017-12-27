Namespace Dto.OrderManagement
    Public Class OrderSearchConditionsDto

        ''' <summary>
        ''' 客户编号
        ''' </summary>
        Public Property CustomerId As Integer

        ''' <summary>
        ''' 订单开始日期
        ''' </summary>
        ''' <returns></returns>
        Public Property OrderDateFrom As Date?

        ''' <summary>
        ''' 订单结束日期
        ''' </summary>
        ''' <returns></returns>
        Public Property OrderDateTo As Date?

    End Class
End Namespace