Namespace Dto.OrderBlog
    Public Class PostDto

        Public Property Id As Integer
        Public Property Title As String
        Public Property ShortDescription As String
        Public Property Description As String
        Public Property Meta As String
        Public Property UrlSlug As String
        Public Property Published As Boolean
        Public Property PostedOn As Nullable(Of Date)
        Public Property Modified As Nullable(Of Date)
        Public Property CategoryId As Integer
        Public Property Author As String

    End Class
End Namespace