Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports OrderManagement.Entities

Namespace Controllers
    Public Class Orders1Controller
        Inherits System.Web.Mvc.Controller

        Private db As New OrderManagementEntities

        ' GET: Orders1
        Function Index() As ActionResult
            Dim orders = db.Orders.Include(Function(o) o.Customer)
            Return View(orders.ToList())
        End Function

        ' GET: Orders1/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim order As Order = db.Orders.Find(id)
            If IsNothing(order) Then
                Return HttpNotFound()
            End If
            Return View(order)
        End Function

        ' GET: Orders1/Create
        Function Create() As ActionResult
            ViewBag.CustomerId = New SelectList(db.Customers, "CustomerId", "Name")
            Return View()
        End Function

        ' POST: Orders1/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="OrderId,CustomerId,OrderType,OrderDate,ShipDate")> ByVal order As Order) As ActionResult
            If ModelState.IsValid Then
                db.Orders.Add(order)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.CustomerId = New SelectList(db.Customers, "CustomerId", "Name", order.CustomerId)
            Return View(order)
        End Function

        ' GET: Orders1/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim order As Order = db.Orders.Find(id)
            If IsNothing(order) Then
                Return HttpNotFound()
            End If
            ViewBag.CustomerId = New SelectList(db.Customers, "CustomerId", "Name", order.CustomerId)
            Return View(order)
        End Function

        ' POST: Orders1/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="OrderId,CustomerId,OrderType,OrderDate,ShipDate")> ByVal order As Order) As ActionResult
            If ModelState.IsValid Then
                db.Entry(order).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.CustomerId = New SelectList(db.Customers, "CustomerId", "Name", order.CustomerId)
            Return View(order)
        End Function

        ' GET: Orders1/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim order As Order = db.Orders.Find(id)
            If IsNothing(order) Then
                Return HttpNotFound()
            End If
            Return View(order)
        End Function

        ' POST: Orders1/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim order As Order = db.Orders.Find(id)
            db.Orders.Remove(order)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
