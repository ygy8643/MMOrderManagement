Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports AutoMapper
Imports OrderManagement.Dtos
Imports OrderManagement.Entities

Namespace Controllers
    Public Class OrdersController
        Inherits System.Web.Http.ApiController

        Private db As New OrderManagementEntities

        ' GET: api/Orders
        Function GetOrders() As List(Of OrderDto)
            Return Mapper.Map(Of List(Of OrderDto))(db.Orders.ToList())
        End Function

        ' GET: api/Orders/5
        <ResponseType(GetType(OrderDto))>
        Function GetOrder(ByVal id As Integer) As IHttpActionResult
            Dim order As Order = db.Orders.Find(id)
            If IsNothing(order) Then
                Return NotFound()
            End If

            Return Ok(Mapper.Map(order, GetType(Order), GetType(OrderDto)))
        End Function

        ' PUT: api/Orders/5
        <ResponseType(GetType(Void))>
        Function PutOrder(ByVal id As Integer, ByVal order As OrderDto) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = order.OrderId Then
                Return BadRequest()
            End If

            db.Entry(order).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (OrderExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Orders
        <ResponseType(GetType(OrderDto))>
        Function PostOrder(ByVal orderdto As OrderDto) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Orders.Add(Mapper.Map(orderdto, GetType(OrderDto)， GetType(Order)))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                If (OrderExists(orderdto.OrderId)) Then
                    Return Conflict()
                Else
                    Throw
                End If
            End Try

            Return CreatedAtRoute("DefaultApi", New With {.id = orderdto.OrderId}, orderdto)
        End Function

        ' DELETE: api/Orders/5
        <ResponseType(GetType(Order))>
        Function DeleteOrder(ByVal id As Integer) As IHttpActionResult
            Dim order As Order = db.Orders.Find(id)
            If IsNothing(order) Then
                Return NotFound()
            End If

            db.Orders.Remove(order)
            db.SaveChanges()

            Return Ok(order)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function OrderExists(ByVal id As Integer) As Boolean
            Return db.Orders.Count(Function(e) e.OrderId = id) > 0
        End Function
    End Class
End Namespace