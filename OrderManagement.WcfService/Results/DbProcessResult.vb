Namespace Results

    Public Class DbProcessResult
        ''' <summary>
        ''' 処理結果
        ''' </summary>
        Public Property IsSuccess As Boolean

        ''' <summary>
        ''' エラーメッセージ
        ''' </summary>
        Public Property ErrorMessage As String

        ''' <summary>
        ''' コンストラクター
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            IsSuccess = True

            ErrorMessage = String.Empty

        End Sub
    End Class
End Namespace