Imports Microsoft.Office.Interop.Excel

Namespace ExcelExport
    ''' <summary>
    '''     Excel出力インターフェース
    ''' </summary>
    Public Interface IExcelHelper

#Region "メソッド"

        ''' <summary>
        '''     出力フォーマット変換
        ''' </summary>
        Sub Convert2Export()

        ''' <summary>
        '''     入力フォーマット変換
        ''' </summary>
        Sub Convert2Import()

        ''' <summary>
        '''     出力
        ''' </summary>
        Sub Export()

        ''' <summary>
        '''     入力
        ''' </summary>
        Sub Import()

        ''' <summary>
        '''     出力ファイルの作成
        ''' </summary>
        ''' <returns></returns>
        Function CreateExcelFile() As Boolean

        ''' <summary>
        '''     タイトル出力
        ''' </summary>
        Sub ExportTitle(dt As Data.DataTable,
                        ByRef xlsWorkCells As Range)

        ''' <summary>
        '''     データ出力
        ''' </summary>
        Sub ExportData(dt As Data.DataTable,
                       ByRef xlsWorkCells As Range)

        ''' <summary>
        '''     表示スタイルをセット
        ''' </summary>
        Sub SetDisplayStyle(dt As Data.DataTable,
                            ByRef xlsWorkSheet As Worksheet)

#End Region
    End Interface
End Namespace