Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Migrations
Imports AutoMapper
Imports OrderManagement.Common
Imports OrderManagement.Entities
Imports OrderManagement.WcfService.Dto

Public Class OrderManagementService
    Implements IOrderManagementService

#Region "Order"

    ''' <summary>
    '''     返回订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDto(orderId As Integer) As OrderDto Implements IOrderManagementService.GetOrderDto
        Using db As New OrderEntities
            Return Mapper.Map(Of OrderDto)(
                db.Orders.Include("Customer").Include("OrderDetails").
                                               SingleOrDefault(Function(o) o.OrderId = orderId))
        End Using
    End Function

    ''' <summary>
    '''     返回所有订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDtoes() As List(Of OrderDto) Implements IOrderManagementService.GetOrderDtoes

        Using db As New OrderEntities
            Return Mapper.Map(Of List(Of OrderDto))(db.Orders.Include("Customer").Include("OrderDetails"))
        End Using
    End Function

    ''' <summary>
    '''     按检索条件返回所有订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDtoesByConditions(conditions As OrderSearchConditionsDto) As List(Of OrderDto) _
        Implements IOrderManagementService.GetOrderDtoesByConditions

        '开始日
        If conditions.OrderDateFrom Is Nothing Then
            conditions.OrderDateFrom = Now.Date.AddYears(-10).Date
        End If

        '终止日
        If conditions.OrderDateTo Is Nothing Then
            conditions.OrderDateTo = Now.Date.AddYears(10).Date
        Else
            conditions.OrderDateTo = conditions.OrderDateTo.Value.AddDays(1).Date
        End If

        Using db As New OrderEntities

            Dim result = Mapper.Map(Of List(Of OrderDto))(db.Orders.Include("Customer").Include("OrderDetails").Where(
                Function(order) (
                                    (String.IsNullOrEmpty(conditions.CustomerId) OrElse
                                     order.CustomerId = conditions.CustomerId)) AndAlso
                                order.OrderDate >= conditions.OrderDateFrom AndAlso
                                order.OrderDate.Value < conditions.OrderDateTo
                ).ToList()
                                                           )

            Return result
        End Using
    End Function

    ''' <summary>
    '''     添加订单
    ''' </summary>
    ''' <param name="orderDto"></param>
    ''' <returns></returns>
    Public Function AddOrderDto(orderDto As OrderDto) As ProcessResult _
        Implements IOrderManagementService.AddOrderDto
        Dim result As New ProcessResult

        Using db As New OrderEntities

            Dim customer = db.Customers.Find(orderDto.CustomerId)
            If customer IsNot Nothing Then
                db.Customers.Attach(customer)
            End If

            db.Orders.AddOrUpdate(Mapper.Map(Of Order)(orderDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If CustomerDtoExists(orderDto.OrderId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    ''' </summary>
    ''' <param name="orderDto"></param>
    ''' <returns></returns>
    Public Function UpdateOrderDto(orderDto As OrderDto) As ProcessResult _
        Implements IOrderManagementService.UpdateOrderDto

        Dim result As New ProcessResult

        Using db As New OrderEntities

            db.Orders.Attach(Mapper.Map(Of Order)(orderDto))
            db.Entry(Mapper.Map(Of Order)(orderDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

    ''' <summary>
    '''     删除订单
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <returns></returns>
    Public Function DeleteOrderDto(orderId As String) As ProcessResult _
        Implements IOrderManagementService.DeleteOrderDto
        Dim result As New ProcessResult

        Using db As New OrderEntities
            Dim order = db.Orders.Find(orderId)

            If IsNothing(order) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Orders.Remove(order)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     订单存在
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <returns></returns>
    Public Function OrderDtoExists(orderId As String) As Boolean Implements IOrderManagementService.OrderDtoExists
        Using db As New OrderEntities
            Return db.Orders.Count(Function(order) order.OrderId = orderId) > 0
        End Using
    End Function

#End Region

#Region "Customer"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="customerDto"></param>
    ''' <returns></returns>
    Public Function AddCustomerDto(customerDto As CustomerDto) As ProcessResult _
        Implements IOrderManagementService.AddCustomerDto
        Dim result As New ProcessResult

        Using db As New OrderEntities
            db.Customers.Add(Mapper.Map(Of Customer)(customerDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If CustomerDtoExists(customerDto.CustomerId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <returns></returns>
    Public Function CustomerDtoExists(customerId As String) As Boolean _
        Implements IOrderManagementService.CustomerDtoExists
        Using db As New OrderEntities
            Return db.Customers.Count(Function(customer) customer.CustomerId = customerId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <returns></returns>
    Public Function DeleteCustomerDto(customerId As String) As ProcessResult _
        Implements IOrderManagementService.DeleteCustomerDto
        Dim result As New ProcessResult

        Using db As New OrderEntities
            Dim customer = db.Customers.Find(customerId)

            If IsNothing(customer) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Customers.Remove(customer)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <returns></returns>
    Public Function GetCustomerDto(customerId As Integer) As CustomerDto _
        Implements IOrderManagementService.GetCustomerDto
        Using db As New OrderEntities
            Dim customer = db.Customers.Find(customerId)
            Return Mapper.Map(Of CustomerDto)(customer)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetCustomerDtoByCondition(condition As CustomerDto) As IEnumerable(Of CustomerDto) _
        Implements IOrderManagementService.GetCustomerDtoByCondition

        Using db As New OrderEntities
            With condition
                Dim result = Mapper.Map(Of List(Of CustomerDto))(
                    db.Customers.Where(
                        Function(c) (.CustomerId = 0 OrElse c.CustomerId = .CustomerId) And
                                    (String.IsNullOrEmpty(.Name) OrElse c.Name = .Name) And
                                    (String.IsNullOrEmpty(.WechatName) OrElse c.Name = .WechatName) And
                                    (String.IsNullOrEmpty(.TaobaoName) OrElse c.Name = .TaobaoName) And
                                    (String.IsNullOrEmpty(.Address) OrElse c.Name = .Address) And
                                    (String.IsNullOrEmpty(.PostCode) OrElse c.Name = .PostCode) And
                                    (String.IsNullOrEmpty(.Phone) OrElse c.Name = .Phone)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    Public Function GetCustomerDtoes() As IEnumerable(Of CustomerDto) _
        Implements IOrderManagementService.GetCustomerDtoes

        Using db As New OrderEntities
            Return Mapper.Map(Of List(Of CustomerDto))(db.Customers.ToList)
        End Using
    End Function

    Public Function UpdateCustomerDto(customerDto As CustomerDto) As ProcessResult _
        Implements IOrderManagementService.UpdateCustomerDto

        Dim result As New ProcessResult

        Using db As New OrderEntities

            db.Customers.Attach(Mapper.Map(Of Customer)(customerDto))
            db.Entry(Mapper.Map(Of Customer)(customerDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region
End Class
