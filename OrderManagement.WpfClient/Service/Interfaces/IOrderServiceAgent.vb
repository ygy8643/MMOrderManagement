Imports OrderManagement.Client.Entities.Models.OrderManagement
Imports OrderManagement.Client.Entities.SearchConditions
Imports OrderManagement.Common

Namespace Service
    Public Interface IOrderServiceAgent
        ''' <summary>
        '''     检索订单信息
        ''' </summary>
        ''' <returns></returns>
        Function GetOrder(orderId As Integer) As OrderClient

        ''' <summary>
        '''     检索订单信息
        ''' </summary>
        ''' <returns></returns>
        Function GetOrders() As IEnumerable(Of OrderClient)

        ''' <summary>
        '''     条件检索订单信息
        ''' </summary>
        ''' <param name="orderSearchConditionsClient"></param>
        ''' <returns></returns>
        Function GetOrdersByConditions(orderSearchConditionsClient As OrderSearchConditionsClient) _
            As IEnumerable(Of OrderClient)

        ''' <summary>
        '''     添加订单
        ''' </summary>
        ''' <param name="order">订单</param>
        ''' <returns></returns>
        Function AddOrder(order As OrderClient) As ProcessResult

        ''' <summary>
        '''     更新订单
        ''' </summary>
        ''' <param name="order">订单</param>
        ''' <returns></returns>
        Function UpdateOrder(order As OrderClient) As ProcessResult

        ''' <summary>
        '''     保存订单
        ''' </summary>
        ''' <param name="order">订单</param>
        ''' <returns></returns>
        Function AddOrUpdateOrder(order As OrderClient) As ProcessResult

        ''' <summary>
        '''     删除订单
        ''' </summary>
        ''' <param name="order">订单</param>
        ''' <returns></returns>
        Function DeleteOrder(order As OrderClient) As ProcessResult
    End Interface
End Namespace

