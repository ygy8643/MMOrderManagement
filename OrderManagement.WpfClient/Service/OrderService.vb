Imports AutoMapper
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Client.Entities.SearchConditions
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service

    Public Class OrderService
        Implements IOrderService

        Private Property Service As New WcfService.OrderManagementServiceClient

        ''' <summary>
        ''' Get Orders
        ''' </summary>
        ''' <returns></returns>
        Public Function GetOrders() As IEnumerable(Of OrderClient) Implements IOrderService.GetOrders

            Return Mapper.Map(Of List(Of OrderClient))(Service.GetOrderDtoes())

        End Function

        ''' <summary>
        ''' Get Orders By Conditions
        ''' </summary>
        ''' <param name="orderSearchConditionsClient"></param>
        ''' <returns></returns>
        Public Function GetOrdersByConditions(orderSearchConditionsClient As OrderSearchConditionsClient) As IEnumerable(Of OrderClient) Implements IOrderService.GetOrdersByConditions
            Return Mapper.Map(Of List(Of OrderClient))(Service.GetOrderDtoesByConditions(Mapper.Map(Of OrderSearchConditionsDto)(orderSearchConditionsClient)))
        End Function

        ''' <summary>
        ''' Get Customers
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCustomerComboBoxList() As IEnumerable(Of ValueNamePair) Implements IOrderService.GetCustomerComboBoxList
            Dim result As New List(Of ValueNamePair)

            Dim lstCustomer = Service.GetCustomerDtoes()

            Dim query = From ctx In lstCustomer
                        Select New ValueNamePair With {.Value = ctx.CustomerId, .DisplayName = ctx.Name}

            result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = String.Empty, .DisplayName = String.Empty})

            Return result

        End Function

    End Class
End Namespace

