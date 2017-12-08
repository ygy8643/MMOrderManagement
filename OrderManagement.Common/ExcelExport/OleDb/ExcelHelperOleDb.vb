Imports System.IO

Namespace ExcelExport.OleDb
    Public Class ExcelHelperOleDb
    
#Region "プロパティ"

        ''' <summary>ダウンロードデータ</summary>
        Public Property Dt As DataTable

        ''' <summary>ダウンロードデータ(複数シート)</summary>
        Public Property Ds As DataSet

        ''' <summary>ダウンロードパス</summary>
        Public Property DownloadPath As String

        ''' <summary>ダウンロード件数</summary>
        Public Property ResultDataCount As Integer

        ''' <summary>ダウンロードファイルサイズ</summary>
        Public Property ResultFileSize As Integer

        ''' <summary>ダウンロード最大件数</summary>
        Public Property LimitDataCount As Integer

        ''' <summary>ダウンロード最大ファイルサイズ</summary>
        Public Property LimitFileSize As Integer

#End Region

#Region "コンストラクタ"

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="dt">ダウンロードデータ</param>
        ''' <param name="dlPath">ダウンロードファイルの物理パス</param>
        ''' <param name="intMaxDataCount">最大ダウンロード件数</param>
        ''' <param name="intMaxFileSize">最大ダウンロードサイズ</param>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Sub New( _
                       ByVal dt As DataTable, _
                       ByVal dlPath As String, _
                       ByVal intMaxDataCount As Integer, _
                       ByVal intMaxFileSize As Integer _
                       )
            SetdtEscapeChar(dt)
            _Dt = dt
            _Ds = Nothing
            _DownloadPath = dlPath
            _LimitDataCount = intMaxDataCount
            _LimitFileSize = intMaxFileSize
        End Sub

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="ds">ダウンロードデータ</param>
        ''' <param name="dlPath">ダウンロードファイルの物理パス</param>
        ''' <param name="intMaxDataCount">最大ダウンロード件数</param>
        ''' <param name="intMaxFileSize">最大ダウンロードサイズ</param>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Sub New( _
                       ByVal ds As DataSet, _
                       ByVal dlPath As String, _
                       ByVal intMaxDataCount As Integer, _
                       ByVal intMaxFileSize As Integer _
                       )
            SetdsEscapeChar(ds)
            _Ds = ds
            _Dt = Nothing
            _DownloadPath = dlPath
            _LimitDataCount = intMaxDataCount
            _LimitFileSize = intMaxFileSize

        End Sub

#End Region

#Region "OLEDBによりExcelファイル作成関連"

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' データをExcelファイルに出力する
        ''' </summary>
        ''' <param name="strConnection">接続文字列</param>
        ''' <param name="strSheetName">出力先のシート名</param>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Function CreateExcel(ByVal strConnection As String, _
                                    ByVal strSheetName As String, _
                                    Optional ByVal macroPath As String = "") As CreateResult

            Dim ret As New CreateResult

            With "事前検証"
                ret.ErrType = ValidateBefore()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            If macroPath.Length > 0 Then
                'マクロファイルのコピー
                File.Copy(macroPath, _DownloadPath, True)

                Dim fileAttr As FileAttributes = File.GetAttributes(_DownloadPath)

                '読み取り専用属性を解除する
                If (fileAttr And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                    File.SetAttributes(_DownloadPath, fileAttr Xor FileAttributes.ReadOnly)
                End If
            Else
                '既存ファイルの削除
                File.Delete(_DownloadPath)
            End If

            'エクセルファイル作成
            Using da As DataAccess = DBMethods.CreateOleDbAccess(strConnection, _DownloadPath)

                ExcelDAL.CreateExcel(da, _Dt, strSheetName)

            End Using

            With "事後検証"
                ret.ErrType = ValidateAfter()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            Return ret

        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' データをExcelファイルに出力する
        ''' </summary>
        ''' <param name="strConnection">接続文字列</param>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Function CreateExcelMultiSheets( _
                                               ByVal strConnection As String, _
                                               Optional ByVal macroPath As String = "" _
                                               ) As CreateResult

            Dim ret As New CreateResult

            If macroPath.Length > 0 Then
                'マクロファイルのコピー
                File.Copy(macroPath, _DownloadPath, True)

                Dim fileAttr As FileAttributes = File.GetAttributes(_DownloadPath)

                '読み取り専用属性を解除する
                If (fileAttr And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                    File.SetAttributes(_DownloadPath, fileAttr Xor FileAttributes.ReadOnly)
                End If

            Else
                '既存ファイルの削除
                File.Delete(_DownloadPath)
            End If

            'エクセルファイル作成
            Using da As DataAccess = DBMethods.CreateOleDbAccess(strConnection, _DownloadPath)
                For Each dtData As DataTable In _Ds.Tables
                    ExcelDAL.CreateExcel(da, dtData, dtData.TableName)
                Next
            End Using

            With "事後検証"
                ret.ErrType = ValidateAfter()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            Return ret

        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' テンプレートを使ってExcelファイルを作成
        ''' </summary>
        ''' <param name="strConnection">接続文字列</param>
        ''' <param name="strTemplatePath">テンプレートパス</param>
        ''' <param name="intStartRow">出力開始行番号</param>
        ''' <param name="intStartColumn">出力開始列番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Function CreateExcelUseTemplate(ByVal strConnection As String, _
                                               ByVal strTemplatePath As String, _
                                               ByVal intStartRow As Integer, _
                                               ByVal intStartColumn As Integer) As CreateResult
            Dim ret As New CreateResult

            With "事前検証"
                ret.ErrType = ValidateBefore()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            ''テンプレートファイルのコピー
            'If Not IO.File.Exists(_DownloadPath) Then
            '    IO.File.Copy(strTemplatePath, _DownloadPath, True)
            'End If

            Dim fileAttr As FileAttributes = File.GetAttributes(_DownloadPath)

            '読み取り専用属性を解除する
            If (fileAttr And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                File.SetAttributes(_DownloadPath, fileAttr Xor FileAttributes.ReadOnly)
            End If

            'エクセルファイル作成
            Using da As DataAccess = DBMethods.CreateOleDBAccess(strConnection, _DownloadPath, False)
                ExcelDAL.UpdateExcel(da, _Dt, _Dt.TableName, intStartRow, intStartColumn)
            End Using

            With "事後検証"
                ret.ErrType = ValidateAfter()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            Return ret

        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' テンプレートのセルを更新
        ''' </summary>
        ''' <param name="strConnection">接続文字列</param>
        ''' <param name="strTemplatePath">テンプレートパス</param>
        ''' <param name="lstCells">セル情報</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Function UpdateExcelByCells(ByVal strConnection As String, _
                                           ByVal strTemplatePath As String, _
                                           ByVal lstCells As List(Of ExcelCell)) As CreateResult
            Dim ret As New CreateResult

            SetListEscapeChar(lstCells)

            'テンプレートファイルのコピー
            If File.Exists(_DownloadPath) Then

                'ファイルが存在する時に削除してからコピー
                File.Delete(_DownloadPath)
                File.Copy(strTemplatePath, _DownloadPath, True)

            Else

                File.Copy(strTemplatePath, _DownloadPath, True)

            End If

            Dim fileAttr As FileAttributes = File.GetAttributes(_DownloadPath)

            With "事前検証"
                ret.ErrType = ValidateBefore()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            '読み取り専用属性を解除する
            If (fileAttr And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
                File.SetAttributes(_DownloadPath, fileAttr Xor FileAttributes.ReadOnly)
            End If

            'エクセルファイル作成
            Using da As DataAccess = DBMethods.CreateOleDBAccess(strConnection, _DownloadPath, False)
                ExcelDAL.UpdateExcelByCells(da, _Dt.TableName, lstCells)
            End Using

            With "事後検証"
                ret.ErrType = ValidateAfter()

                If Not ret.IsSuccess Then
                    Return ret
                End If
            End With

            Return ret

        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' 事前検証
        ''' </summary>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Private Function ValidateBefore() As CreateResult.ErrorType

            ' 結果件数の取得
            If Not _Ds Is Nothing Then
                For Each dtTmp As DataTable In _Ds.Tables
                    _ResultDataCount += dtTmp.Rows.Count
                Next
            Else
                _ResultDataCount = _Dt.Rows.Count
            End If

            Select Case _ResultDataCount
                Case 0
                    'データ０件
                    Return CreateResult.ErrorType.NODATA

                Case Is > _LimitDataCount
                    '制限件数オーバー
                    Return CreateResult.ErrorType.DatacountOver

            End Select

            Return CreateResult.ErrorType.NONE

        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        ''' 事後検証
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Private Function ValidateAfter() As CreateResult.ErrorType

            ' ファイルサイズの取得
            _ResultFileSize = New FileInfo(_DownloadPath).Length

            If _ResultFileSize > _LimitFileSize Then
                '制限サイズオーバー

                'ファイルの削除
                File.Delete(_DownloadPath)

                Return CreateResult.ErrorType.FilesizeOver

            End If

            Return CreateResult.ErrorType.NONE

        End Function

#End Region

#Region "エスケープ処理"

        Private Sub SetdtEscapeChar(ByRef dt As DataTable)
            For Each row In dt.Rows
                For Each col In dt.Columns
                    Dim t As Type = col.GetType()

                    If t.Equals(GetType(String)) Then
                        row(col) = row(col).ToString().Replace("'", "''")
                    End If
                Next
            Next
        End Sub

        Private Sub SetdsEscapeChar(ByRef ds As DataSet)
            For Each dtSub In ds.Tables
                SetdtEscapeChar(dtSub)
            Next
        End Sub

        Private Sub SetListEscapeChar(ByRef lst As List(Of ExcelCell))
            For Each value In lst
                value.Value = value.Value.Replace("'", "''")
            Next
        End Sub

#End Region

#Region "Microsoft.Office.InteropによりExcelファイル作成関連"
        ' ''' ---------------------------------------------------------------------------------------
        ' ''' <summary>
        ' ''' Excelに出力する
        ' ''' </summary>
        ' ''' <param name="dsData">出力データ</param>
        ' ''' <param name="strTempFilePath">テンプレートファイルパス</param>
        ' ''' <param name="strOutPutFilePath">出力ファイルパス</param>
        ' ''' <param name="blnProtect">ファイル保護フラグ</param>
        ' ''' <param name="blnFrame">各セルの枠</param>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        ' ''' ---------------------------------------------------------------------------------------
        'Public Shared Function ExportToExcel(ByVal dsData As DataSet, ByVal strTempFilePath As String, _
        '                                     ByVal strOutPutFilePath As String, ByVal blnProtect As Boolean, _
        '                                     ByVal blnFrame As Boolean) As Boolean
        '    Dim blnResult As Boolean = True
        '    Dim xls As New Excel.Application
        '    Dim xlsWorkBooks As Excel.Workbooks
        '    Dim xlsWorkBook As Excel.Workbook
        '    Dim xlsWorkSheets As Excel.Sheets
        '    Dim xlsWorkSheet As Excel.Worksheet
        '    Dim xlsWorkCells As Excel.Range

        '    'ファイルをコピーする
        '    IO.File.Copy(strTempFilePath, strOutPutFilePath, True)

        '    'ファイルを開く
        '    xlsWorkBooks = xls.Workbooks
        '    xlsWorkBook = xlsWorkBooks.Open(strOutPutFilePath)
        '    xlsWorkSheet = xlsWorkBook.Sheets("Template")
        '    xlsWorkCells = xlsWorkSheet.Cells

        '    'テーブル毎に出力
        '    For Each dt As DataTable In dsData.Tables
        '        'テンプレートシートをコピー
        '        xlsWorkSheets = xlsWorkBook.Sheets

        '        xlsWorkSheet.Copy(Type.Missing, xlsWorkBook.Sheets(xlsWorkSheets.Count))

        '        xlsWorkSheet = xlsWorkBook.Sheets.Item(xlsWorkSheets.Count)
        '        xlsWorkSheet.Name = "インフルエンザ台帳"
        '        xlsWorkCells = xlsWorkSheet.Cells

        '        If dt.Rows.Count <= 0 Then
        '            'データがない場合は次のテーブルへ
        '            Continue For
        '        Else
        '            'Cellごとに設定する
        '            For intRow As Integer = 0 To dt.Rows.Count - 1
        '                For intColumn As Integer = 0 To dt.Columns.Count - 1
        '                    Dim strColumnName As String = dt.Columns(intColumn).ColumnName
        '                    'Excelの行番号が"1"からです
        '                    xlsWorkCells(intRow + 2, intColumn + 1) = dt.Rows(intRow).Item(strColumnName).ToString
        '                Next
        '            Next
        '        End If
        '    Next

        '    'シートの保護
        '    If blnProtect Then
        '        xlsWorkSheet.Protect()
        '    End If

        '    'テンプレートの削除
        '    xls.DisplayAlerts = False
        '    xlsWorkSheet = xlsWorkBook.Sheets("Template")
        '    xlsWorkSheet.Delete()

        '    xlsWorkBook.Save()
        '    xlsWorkBook.Close(True, Type.Missing, Type.Missing)
        '    xls.Quit()
        '    MRComObject(xlsWorkCells)
        '    MRComObject(xlsWorkSheet)
        '    MRComObject(xlsWorkBook)
        '    MRComObject(xlsWorkBooks)
        '    MRComObject(xls)

        '    GC.Collect()

        '    Return blnResult
        'End Function

        ' ''' ---------------------------------------------------------------------------------------
        ' ''' <summary>
        ' ''' COMオブジェクト開放処理
        ' ''' </summary>
        ' ''' <param name="objCom"></param>
        ' ''' <remarks></remarks>
        ' ''' ''' ---------------------------------------------------------------------------------------
        'Public Shared Sub MRComObject(ByRef objCom As Object)
        '    Dim intLoopCnt As Integer
        '    Try
        '        If objCom IsNot Nothing Then
        '            '提供されたランタイム呼び出し可能ラッパーの参照カウントをデクリメントします
        '            If Not objCom Is Nothing AndAlso System.Runtime.InteropServices.Marshal.IsComObject(objCom) Then
        '                Do
        '                    intLoopCnt = System.Runtime.InteropServices.Marshal.ReleaseComObject(objCom)
        '                Loop Until intLoopCnt <= 0
        '            End If
        '        End If
        '    Catch ex As Exception
        '    Finally
        '        objCom = Nothing
        '    End Try
        'End Sub
#End Region

    End Class
End NameSpace