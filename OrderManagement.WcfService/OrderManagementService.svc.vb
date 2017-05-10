Imports OrderManagement.Entities
Imports OrderManagement.WcfService.Dto
Imports AutoMapper

Public Class OrderManagementService
    Implements IOrderManagementService

    ''' <summary>
    ''' 返回所有订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDtoes() As List(Of OrderDto) Implements IOrderManagementService.GetOrderDtoes

        Try
            Using dbContext As New OrderManagementEntities

                Return Mapper.Map(Of List(Of OrderDto))(dbContext.Orders.ToList)

            End Using
        Catch ex As Exception
            Dim strError As String = ex.Message
        End Try

    End Function
End Class
