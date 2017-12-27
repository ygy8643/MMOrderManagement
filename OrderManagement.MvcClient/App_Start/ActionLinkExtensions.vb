Imports System.Runtime.CompilerServices
Imports OrderManagement.MvcClient.WcfService
Imports System.Web.Mvc
Imports System.Web.Mvc.Html

Public Module ActionLinkExtensions
    <Extension()>
    Public Function PostLink(ByVal htmlHelper As HtmlHelper, post As PostDto) As Object
        Return htmlHelper.ActionLink(post.Title, "Details", "Blog", post.Id, "")
    End Function

    <Extension()>
    Public Function CategoryLink(ByVal htmlHelper As HtmlHelper, category As CategoryDto) As Object
        Return htmlHelper.ActionLink(category.Name, "Category", "Blog", New With {.categoryId = category.Id}, New With {.class = "list-group-item"})
    End Function

    <Extension()>
    Public Function TagLink(ByVal htmlHelper As HtmlHelper, tag As TagDto) As Object
        'Return htmlHelper.ActionLink(tag.Name, "Details", "Blog", tag.Id)
    End Function
End Module
