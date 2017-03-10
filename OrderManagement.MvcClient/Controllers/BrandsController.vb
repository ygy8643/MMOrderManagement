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
    Public Class BrandsController
        Inherits System.Web.Mvc.Controller

        Private db As New OrderManagementEntities

        ' GET: Brands
        Function Index() As ActionResult
            Return View(db.Brands.ToList())
        End Function

        ' GET: Brands/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim brand As Brand = db.Brands.Find(id)
            If IsNothing(brand) Then
                Return HttpNotFound()
            End If
            Return View(brand)
        End Function

        ' GET: Brands/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Brands/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="BrandId,BrandName,BrandNameJp")> ByVal brand As Brand) As ActionResult
            If ModelState.IsValid Then
                db.Brands.Add(brand)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(brand)
        End Function

        ' GET: Brands/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim brand As Brand = db.Brands.Find(id)
            If IsNothing(brand) Then
                Return HttpNotFound()
            End If
            Return View(brand)
        End Function

        ' POST: Brands/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="BrandId,BrandName,BrandNameJp")> ByVal brand As Brand) As ActionResult
            If ModelState.IsValid Then
                db.Entry(brand).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(brand)
        End Function

        ' GET: Brands/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim brand As Brand = db.Brands.Find(id)
            If IsNothing(brand) Then
                Return HttpNotFound()
            End If
            Return View(brand)
        End Function

        ' POST: Brands/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim brand As Brand = db.Brands.Find(id)
            db.Brands.Remove(brand)
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
