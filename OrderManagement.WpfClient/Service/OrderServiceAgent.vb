Imports AutoMapper
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Client.Entities.SearchConditions
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service

    Public Class OrderServiceAgent
        Implements IOrderServiceAgent

        Private ReadOnly _service As New OrderManagementServiceClient

        Public Function AddOrder(order As OrderClient) As ProcessResult Implements IOrderServiceAgent.AddOrder
            Return _service.AddOrderDto(Mapper.Map(Of OrderDto)(order))
        End Function

        Public Function DeleteOrder(order As OrderClient) As ProcessResult Implements IOrderServiceAgent.DeleteOrder
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Get Orders
        ''' </summary>
        ''' <returns></returns>
        Public Function GetOrders() As IEnumerable(Of OrderClient) Implements IOrderServiceAgent.GetOrders
            Return Mapper.Map(Of List(Of OrderClient))(_service.GetOrderDtoes())
        End Function

        ''' <summary>
        ''' Get Orders By Conditions
        ''' </summary>
        ''' <param name="orderSearchConditionsClient"></param>
        ''' <returns></returns>
        Public Function GetOrdersByConditions(orderSearchConditionsClient As OrderSearchConditionsClient) As IEnumerable(Of OrderClient) Implements IOrderServiceAgent.GetOrdersByConditions
            Return Mapper.Map(Of List(Of OrderClient))(_service.GetOrderDtoesByConditions(Mapper.Map(Of OrderSearchConditionsDto)(orderSearchConditionsClient)))
        End Function

        Public Function UpdateOrder(order As OrderClient) As ProcessResult Implements IOrderServiceAgent.UpdateOrder
            Return _service.UpdateOrderDto(Mapper.Map(Of OrderDto)(order))
        End Function
    End Class
End Namespace

