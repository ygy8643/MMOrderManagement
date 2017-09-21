Imports OrderManagement.Client.Entities
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Client.Entities.SearchConditions
Imports OrderManagement.Common

Namespace Service

    Public Interface IOrderService

        ''' <summary>
        ''' 检索订单信息
        ''' </summary>
        ''' <returns></returns>
        Function GetOrders() As IEnumerable(Of OrderClient)

        ''' <summary>
        ''' 条件检索订单信息
        ''' </summary>
        ''' <param name="orderSearchConditionsClient"></param>
        ''' <returns></returns>
        Function GetOrdersByConditions(orderSearchConditionsClient As OrderSearchConditionsClient) As IEnumerable(Of OrderClient)

        ''' <summary>
        ''' 顾客信息
        ''' </summary>
        ''' <returns></returns>
        Function GetCustomerComboBoxList() As IEnumerable(Of ValueNamePair)

    End Interface
End Namespace

