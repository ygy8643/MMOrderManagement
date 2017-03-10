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
    Public Class CustomersController
        Inherits System.Web.Mvc.Controller

        Private db As New OrderManagementEntities

        ' GET: Customers
        Function Index() As ActionResult
            Return View(db.Customers.ToList())
        End Function

        ' GET: Customers/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim customer As Customer = db.Customers.Find(id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

        ' GET: Customers/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Customers/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="CustomerId,Name,Address,PostCode,Phone")> ByVal customer As Customer) As ActionResult
            If ModelState.IsValid Then
                db.Customers.Add(customer)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(customer)
        End Function

        ' GET: Customers/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim customer As Customer = db.Customers.Find(id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

        ' POST: Customers/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="CustomerId,Name,Address,PostCode,Phone")> ByVal customer As Customer) As ActionResult
            If ModelState.IsValid Then
                db.Entry(customer).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(customer)
        End Function

        ' GET: Customers/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim customer As Customer = db.Customers.Find(id)
            If IsNothing(customer) Then
                Return HttpNotFound()
            End If
            Return View(customer)
        End Function

        ' POST: Customers/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim customer As Customer = db.Customers.Find(id)
            db.Customers.Remove(customer)
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
