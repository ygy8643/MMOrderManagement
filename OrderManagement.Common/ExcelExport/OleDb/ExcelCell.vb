Namespace ExcelExport.OleDb
    Public Class ExcelCell

#Region "属性"

        ''' <summary>行番号</summary>
        Public Property RowId As Integer

        ''' <summary>列番号</summary>
        Public Property ColumnId As Integer

        ''' <summary>値</summary>
        Public Property Value As String

#End Region

#Region "コンストラクト"

        ''' <summary>
        '''     コンストラクト
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            '行番号
            RowId = 1

            '列番号
            ColumnId = 1

            '値
            Value = String.Empty
        End Sub

        ''' <summary>
        '''     コンストラクト
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(intRowId As Integer, intColumnId As Integer, strValue As String)

            '行番号
            RowId = intRowId

            '列番号
            ColumnId = intColumnId

            '値
            Value = strValue
        End Sub

#End Region
    End Class
End NameSpace