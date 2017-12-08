Namespace ExcelExport.OleDb
    Public Class CreateResult

#Region "定数"

        ''' -------------------------------------------------------------------
        ''' <summary>
        '''     エラー種類
        ''' </summary>
        ''' <remarks></remarks>
        ''' -------------------------------------------------------------------
        Public Enum ErrorType
            ''' <summary>エラーなし（初期値）</summary>
            None

            ''' <summary>件数０件</summary>
            Nodata

            ''' <summary>件数制限オーバー</summary>
            DatacountOver

            ''' <summary>ファイルサイズ制限オーバー</summary>
            FilesizeOver
        End Enum

#End Region


        Private _errType As ErrorType

        ''' ---------------------------------------------------------------
        ''' <summary>
        '''     成功・失敗
        ''' </summary>
        ''' <value>True：成功、False：失敗</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------
        Public ReadOnly Property IsSuccess As Boolean
            Get
                If _errType = ErrorType.NONE Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        ''' ---------------------------------------------------------------
        ''' <summary>
        '''     エラータイプ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------
        Public Property ErrType As ErrorType
            Get
                Return _errType
            End Get
            Set
                _errType = value
            End Set
        End Property

        ''' -----------------------------------------------------------------------------------
        ''' <summary>
        '''     コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        ''' -----------------------------------------------------------------------------------
        Public Sub New()

            _errType = ErrorType.NONE
        End Sub
    End Class
End NameSpace