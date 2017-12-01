Imports System.Net
Imports System.Web.Mvc
Imports AutoMapper
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.MvcClient.WcfService

Namespace Controllers
    Public Class BrandController
        Inherits Controller

        Private ReadOnly _db As New OrderManagementServiceClient

        ' GET: Brand
        Function Index() As ActionResult
            Return View(Mapper.Map (Of List(Of BrandClient))(_db.GetBrandDtoes()))
        End Function

        ' GET: /Brand/Details/5
        Function Details(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim brand = Mapper.Map (of BrandClient )(_db.GetBrandDto(id))
            If IsNothing(Brand) Then
                Return HttpNotFound()
            End If
            Return View(Brand)
        End Function

        ' GET: /Brand/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: /Brand/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Create(<Bind(Include := "BrandName,BrandNameJp")> brand As Brandclient) As ActionResult
            If ModelState.IsValid Then
                _db.AddBrandDto(Mapper.Map (of BrandDto )(brand))
                Return RedirectToAction("Index")
            End If
            Return View(Brand)
        End Function

        ' GET: /Brand/Edit/5
        Function Edit(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim brand = Mapper.Map (of BrandClient)(_db.GetBrandDto(id))
            If IsNothing(Brand) Then
                Return HttpNotFound()
            End If
            Return View(Brand)
        End Function

        ' POST: /Brand/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Edit(<Bind(Include := "BrandName,BrandNameJp")> brand As Brandclient) As ActionResult
            If ModelState.IsValid Then
                _db.UpdateBrandDto(mapper.Map (of BrandDto)(brand))
                Return RedirectToAction("Index")
            End If
            Return View(brand)
        End Function

        ' GET: /Brand/Delete/5
        Function Delete(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim brand = Mapper.Map (of BrandClient)(_db.GetBrandDto(id))
            If IsNothing(Brand) Then
                Return HttpNotFound()
            End If
            Return View(Brand)
        End Function

        ' POST: /Brand/Delete/5
        <HttpPost>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Function DeleteConfirmed(id As Integer) As ActionResult
            _db.DeleteBrandDto(id)
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace