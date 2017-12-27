Imports OrderManagement.MvcClient.WcfService

Namespace Models

    Public Class WidgetViewModel

        ''' <summary>
        ''' カテゴリ
        ''' </summary>
        ''' <returns></returns>
        Public Property Categories As List(Of CategoryDto)

        ''' <summary>
        ''' タグ
        ''' </summary>
        ''' <returns></returns>
        Public Property Tags As List(Of TagDto)

        ''' <summary>
        ''' 最新POST
        ''' </summary>
        ''' <returns></returns>
        Public Property LatestPosts As List(Of PostDto)

        Public Sub New()
            Using db As New OrderManagementServiceClient
                Categories = db.GetCategoryDtoes()
                Tags = db.GetTagDtoes()
                LatestPosts = db.GetLatestPostDtoes(10)
            End Using
        End Sub

    End Class
End Namespace