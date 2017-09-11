Imports OrderManagement.WcfService.Dto
Imports AutoMapper
Imports OrderManagement.Entities

Public Class OrderManagementService
    Implements IOrderManagementService

    ''' <summary>
    ''' 返回所有订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDtoes() As List(Of OrderDto) Implements IOrderManagementService.GetOrderDtoes

        Using dbContext As New OrderEntities
            Return Mapper.Map(Of List(Of OrderDto))(dbContext.Orders.ToList)
        End Using

    End Function
End Class
