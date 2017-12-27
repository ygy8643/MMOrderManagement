Imports System.Net
Imports System.Web.Mvc
Imports AutoMapper
Imports Microsoft.VisualBasic.CompilerServices
Imports OrderManagement.Client.Entities.Models.OrderBlog
Imports OrderManagement.MvcClient.Models
Imports OrderManagement.MvcClient.WcfService
Imports PagedList

Namespace Controllers
    Public Class BlogController
        Inherits Controller

        Private ReadOnly _db As New OrderManagementServiceClient

        ''' <summary>
        '''     一覧
        ''' </summary>
        ''' <param name="currentFilter"></param>
        ''' <param name="page"></param>
        ''' <returns></returns>
        Function Index(currentFilter As String, page As Integer?) As ActionResult

            Dim superset = Mapper.Map(Of List(Of PostClient))(_db.GetPostDtoes())

            Dim pageSize = 10
            Dim pageNumber = IIf(page Is Nothing, 1, page)

            Return View(superset.ToPagedList(pageNumber, pageSize))
        End Function

        ''' <summary>
        '''     一覧
        ''' </summary>
        ''' <param name="categoryId"></param>
        ''' <param name="page"></param>
        ''' <returns></returns>
        Function Category(categoryId As Integer, page As Integer?) As ActionResult

            Dim superset = Mapper.Map(Of list(of PostClient))(_db.GetPostDtoesByCategory(categoryId))

            Dim pageSize = 10
            Dim pageNumber = IIf(page Is Nothing, 1, page)

            Return View("Index", superset.ToPagedList(pageNumber, pageSize))
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="searchString"></param>
        ''' <param name="page"></param>
        ''' <returns></returns>
        Function Search(searchString As String, page As Integer?) As ActionResult
            
            Dim superset = Mapper.Map(Of list(of PostClient))(_db.GetPostDtoesByTitle(searchString))

            Dim pageSize = 10
            Dim pageNumber = IIf(page Is Nothing, 1, page)

            Return View("Index", superset.ToPagedList(pageNumber, pageSize))

        End Function

        ''' <summary>
        ''' 追加
        ''' </summary>
        ''' <returns></returns>
        Function Create() As ActionResult

            NewLateBinding.LateSetComplex(
            ViewBag,
            Nothing,
            "CategoryId",
            New Object() {New SelectList(_db.GetCategoryDtoes(), "Id", "Name")},
            Nothing,
            Nothing,
            False,
            True)

            Return View()
        End Function

        ''' <summary>
        ''' 追加
        ''' </summary>
        ''' <param name="post"></param>
        ''' <returns></returns>
        <HttpPost, ValidateAntiForgeryToken>
        Function Create(<Bind(Include:="Id,CategoryId,Title,ShortDescription,Description,Meta,UrlSlug,Author,Published,PostedOn,Modified")> post As PostClient)

            If ModelState.IsValid Then
                _db.AddPostDto(Mapper.Map(Of PostDto)(post))
                Return RedirectToAction("Index")
            End If

            Return View()
        End Function

        ''' <summary>
        '''     明細
        ''' </summary>
        ''' <param name="id"></param>
        ''' <returns></returns>
        Function Details(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim postClient = Mapper.Map(Of PostClient)(_db.GetPostDto(id))
            If IsNothing(postClient) Then
                Return HttpNotFound()
            End If
            Return View(postClient)
        End Function

        Function Edit(id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim postClient = Mapper.Map(Of PostClient)(_db.GetPostDto(id))
            If IsNothing(postClient) Then
                Return HttpNotFound()
            End If
            NewLateBinding.LateSetComplex(ViewBag,
                                          Nothing,
                                          "CategoryId",
                                          New Object() {New SelectList(_db.GetCategoryDtoes(), "Id", "Name", postClient.Id)},
                                          Nothing,
                                          Nothing,
                                          False,
                                          True)
            Return View(postClient)
        End Function

        <ChildActionOnly>
        Function GetSidebar() As PartialViewResult
            Dim model = New WidgetViewModel

            Return PartialView("_Sidebar", model)
        End Function
    End Class
End Namespace