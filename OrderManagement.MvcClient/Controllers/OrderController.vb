Imports System.Net
Imports System.Web.Mvc
Imports AutoMapper
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.MvcClient.WcfService

Namespace Controllers
    Public Class OrderController
        Inherits Controller

        Private ReadOnly _db As New OrderManagementServiceClient

        ' GET: Order
        Function Index() As ActionResult
            Return View(Mapper.Map(Of List(Of OrderClient))(_db.GetOrderDtoes()))
        End Function

        ' GET: /Order/Details/5
        Function Details(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim order = Mapper.Map(Of OrderClient)(_db.GetOrderDto(id))
            If IsNothing(order) Then
                Return HttpNotFound()
            End If
            Return View(order)
        End Function

        ' GET: /Order/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: /Order/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Create(<Bind(Include:="CustomerId,OrderDate,ShipDate,OrderType,InvoiceNo,Freight")> order As OrderClient) As ActionResult
            If ModelState.IsValid Then
                _db.AddOrderDto(Mapper.Map(Of OrderDto)(order))
                Return RedirectToAction("Index")
            End If
            Return View(order)
        End Function

        ' GET: /Order/Edit/5
        Function Edit(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim order = Mapper.Map(Of OrderClient)(_db.GetOrderDto(id))
            If IsNothing(order) Then
                Return HttpNotFound()
            End If
            Return View(order)
        End Function

        ' POST: /Order/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Edit(<Bind(Include:="CustomerId,OrderDate,ShipDate,OrderType,InvoiceNo,Freight")> order As OrderClient) As ActionResult
            If ModelState.IsValid Then
                _db.UpdateOrderDto(Mapper.Map(Of OrderDto)(order))
                Return RedirectToAction("Index")
            End If
            Return View(order)
        End Function

        ' GET: /Order/Delete/5
        Function Delete(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim order = Mapper.Map(Of OrderClient)(_db.GetOrderDto(id))
            If IsNothing(order) Then
                Return HttpNotFound()
            End If
            Return View(order)
        End Function

        ' POST: /Order/Delete/5
        <HttpPost>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Function DeleteConfirmed(id As Integer) As ActionResult
            _db.DeleteOrderDto(id)
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace