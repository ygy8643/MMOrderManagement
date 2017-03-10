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
    Public Class OrderDetailsController
        Inherits System.Web.Mvc.Controller

        Private db As New OrderManagementEntities

        ' GET: OrderDetails
        Function Index() As ActionResult
            Dim orderDetails = db.OrderDetails.Include(Function(o) o.Order).Include(Function(o) o.Product)
            Return View("../Orders/Details", orderDetails.FirstOrDefault.Order)
        End Function

        ' GET: OrderDetails/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim orderDetail As OrderDetail = db.OrderDetails.Find(id)
            If IsNothing(orderDetail) Then
                Return HttpNotFound()
            End If
            Return View(orderDetail)
        End Function

        ' GET: OrderDetails/Create
        Function Create(ByVal orderId As Integer?) As ActionResult
            ViewBag.OrderIdList = New SelectList(db.Orders, "OrderId", "OrderId")
            ViewBag.OrderId = orderId
            ViewBag.ProductId = New SelectList(db.Products, "ProductId", "ProductName")
            Return View()
        End Function

        ' POST: OrderDetails/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="OrderDetailId,OrderId,ProductId,Quantity,PurchasePrice,SoldPrice,Link")> ByVal orderDetail As OrderDetail) As ActionResult
            If ModelState.IsValid Then
                db.OrderDetails.Add(orderDetail)
                db.SaveChanges()
                'Return RedirectToAction("Index")
                Return RedirectToAction("Details", "Orders", New With {.id = orderDetail.OrderId})
            End If
            ViewBag.OrderId = New SelectList(db.Orders, "OrderId", "OrderId", orderDetail.OrderId)
            ViewBag.ProductId = New SelectList(db.Products, "ProductId", "ProductName", orderDetail.ProductId)

            Return View(orderDetail)
        End Function

        ' GET: OrderDetails/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim orderDetail As OrderDetail = db.OrderDetails.Find(id)
            If IsNothing(orderDetail) Then
                Return HttpNotFound()
            End If
            ViewBag.OrderId = New SelectList(db.Orders, "OrderId", "OrderId", orderDetail.OrderId)
            ViewBag.ProductId = New SelectList(db.Products, "ProductId", "ProductName", orderDetail.ProductId)
            Return View(orderDetail)
        End Function

        ' POST: OrderDetails/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="OrderDetailId,OrderId,ProductId,Quantity,PurchasePrice,SoldPrice,Link")> ByVal orderDetail As OrderDetail) As ActionResult
            If ModelState.IsValid Then
                db.Entry(orderDetail).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Details", "Orders", New With {.id = orderDetail.OrderId})
            End If
            ViewBag.OrderId = New SelectList(db.Orders, "OrderId", "OrderId", orderDetail.OrderId)
            ViewBag.ProductId = New SelectList(db.Products, "ProductId", "ProductName", orderDetail.ProductId)
            Return View(orderDetail)
        End Function

        ' GET: OrderDetails/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim orderDetail As OrderDetail = db.OrderDetails.Find(id)
            If IsNothing(orderDetail) Then
                Return HttpNotFound()
            End If
            Return View(orderDetail)
        End Function

        ' POST: OrderDetails/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim orderDetail As OrderDetail = db.OrderDetails.Find(id)
            db.OrderDetails.Remove(orderDetail)
            db.SaveChanges()
            Return RedirectToAction("Details", "Orders", New With {.id = orderDetail.OrderId})
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
