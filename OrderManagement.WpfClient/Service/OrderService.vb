Imports AutoMapper
Imports OrderManagement.Client.Entities

Namespace Service

    Public Class OrderService
        Implements IOrderService

        Public Function GetOrders() As IEnumerable(Of OrderClient) Implements IOrderService.GetOrders
            Using clent As New WcfService.OrderManagementServiceClient

                Return Mapper.Map(Of List(Of OrderClient))(clent.GetOrderDtoes())

            End Using
        End Function
    End Class
End Namespace

