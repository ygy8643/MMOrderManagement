Imports System.Web.Mvc

Namespace Models.OrderBlog
    Public Class PostClient
        
        Public Property Id As Integer
        Public Property Title As String
        <AllowHtml>
        Public Property ShortDescription As String
        <AllowHtml>
        Public Property Description As String
        Public Property Meta As String
        Public Property UrlSlug As String
        Public Property Published As Boolean
        Public Property PostedOn As Nullable(Of Date)
        Public Property Modified As Nullable(Of Date)
        Public Property CategoryId As Integer
        Public Property Author As String

    End Class

End NameSpace