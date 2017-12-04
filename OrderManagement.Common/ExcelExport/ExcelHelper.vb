Imports System.Data.OleDb
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Excel
Imports OleDbConnection = System.Data.OleDb.OleDbConnection

Namespace ExcelExport
    ''' <summary>
    '''     Excel出力
    ''' </summary>
    Public Class ExcelHelper
        Implements IExcelHelper

#Region "プロパティ"

        ''' <summary>入力ファイル名</summary>
        Public Property ImportFileName As String

        ''' <summary>出力ファイル名</summary>
        Public Property ExportFileName As String

        ''' <summary>シート名</summary>
        Public Property SheetName As String

        ''' <summary>入力・出力データ</summary>
        Public Property DsExcel As DataSet

        ''' <summary>編集可能項目の背景色</summary>
        Public Property BackgroundColor As Color

        ''' <summary>編集可能設定フラグ</summary>
        Public Property SetReadOnlyFlg As Boolean

        ''' <summary>キー項目を隠すフラグ</summary>
        Public Property HideKeysFlg As Boolean

        ''' <summary>枠を表示するフラグ</summary>
        Public Property ShowLineFlg As Boolean

#End Region

#Region "コンストラクター"

        ''' <summary>
        '''     インポート
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Sub New(fileName As String)

            'ファイル名
            ImportFileName = fileName

            '出力・取込データ
            DsExcel = New DataSet

            '背景色
            BackgroundColor = Color.Yellow

            '読み取り専用フラグ
            SetReadOnlyFlg = False

            '主キー列非表示のフラグ
            HideKeysFlg = True

            '枠表示フラグ
            ShowLineFlg = True
        End Sub

        ''' <summary>
        '''     エクスポート
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="dtExport"></param>
        ''' <param name="backgroundColor"></param>
        ''' <param name="setReadOnlyFlg"></param>
        ''' <param name="hideKeysFlg"></param>
        ''' <param name="showLineFlg"></param>
        Public Sub New(fileName As String,
                       sheetName As String,
                       dtExport As Data.DataTable,
                       Optional backgroundColor As Color = Nothing,
                       Optional setReadOnlyFlg As Boolean = False,
                       Optional hideKeysFlg As Boolean = True,
                       Optional showLineFlg As Boolean = True)

            'ファイル名
            ExportFileName = fileName

            'シート名
            Me.SheetName = sheetName
            
            '出力・取込データ
            DsExcel = New DataSet

            '出力・取込データ
            DsExcel.Tables.Add(dtExport)

            '背景色
            Me.BackgroundColor = backgroundColor

            '読み取り専用フラグ
            Me.SetReadOnlyFlg = setReadOnlyFlg

            '主キー列非表示のフラグ
            Me.HideKeysFlg = hideKeysFlg

            '枠表示フラグ
            Me.ShowLineFlg = showLineFlg
        End Sub

#End Region

#Region "メソッド(Public)"

        ''' <summary>
        '''     出力データを変換
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Convert2Export() Implements IExcelHelper.Convert2Export
        End Sub

        ''' <summary>
        '''     取込データを変換
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Convert2Import() Implements IExcelHelper.Convert2Import
        End Sub

        ''' <summary>
        '''     Excelファイルの作成
        ''' </summary>
        ''' <returns></returns>
        Public Function CreateExcelFile() As Boolean Implements IExcelHelper.CreateExcelFile
            Dim blnResult = True
            Dim xlApp As New Application
            Dim xlWorkBook As Workbook
            '追加
            xlWorkBook = xlApp.Workbooks.Add

            Try
                '保存
                xlWorkBook.SaveAs(Filename:=ExportFileName)

                '閉じる
                xlWorkBook.Close()

                xlApp.Quit()

            Catch ex As Exception
                blnResult = False
            Finally
                MrComObject(xlApp)
                MrComObject(xlWorkBook)
            End Try

            Return blnResult
        End Function

        ''' <summary>
        '''     データ出力
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Export() Implements IExcelHelper.Export

            If CreateExcelFile() Then
                Dim xlsApp As New Application
                Dim xlsWorkBook As Workbook

                'ファイルを開く
                xlsWorkBook = xlsApp.Workbooks.Open(ExportFileName)

                '警告表示OFF
                xlsApp.DisplayAlerts = False

                Try
                    'テーブル毎に出力
                    For Each dtExcel In DsExcel.Tables
                        If dtExcel.Rows.Count > 0 Then

                            '追加するシート
                            Dim xlsWorkSheet As Worksheet
                            '追加されたセル
                            Dim xlsWorkCells As Range

                            'テンプレートシートを追加
                            xlsWorkSheet = xlsWorkBook.Worksheets.Add()

                            'シート名
                            xlsWorkSheet.Name = SheetName & DsExcel.Tables.IndexOf(dtExcel)

                            '背景スタイルの設定
                            SetBackColor(xlsWorkSheet)

                            'セルの取得
                            xlsWorkCells = xlsWorkSheet.Cells

                            'タイトルの出力
                            ExportTitle(dtExcel, xlsWorkCells)

                            'データの出力
                            ExportData(dtExcel, xlsWorkCells)

                            '表示スタイルの設定
                            SetDisplayStyle(dtExcel, xlsWorkSheet)

                            '回収
                            MrComObject(xlsWorkSheet)
                            MrComObject(xlsWorkCells)
                        End If
                    Next

                    '保存
                    xlsWorkBook.Save()

                Catch ex As Exception
                    Throw
                Finally
                    xlsWorkBook.Close(True, Type.Missing, Type.Missing)
                    xlsApp.Quit()

                    '回収
                    MrComObject(xlsWorkBook)
                    MrComObject(xlsApp)

                    GC.Collect()

                End Try
            End If
        End Sub

        ''' <summary>
        '''     取込
        ''' </summary>
        Public Sub Import() Implements IExcelHelper.Import

            Dim strConn As String

            'EXCEL接続文字列
            If Path.GetExtension(ImportFileName).ToLower.Equals(".xls") Then
                strConn =
                    String.Format(
                        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=NO;IMEX=1;'",
                        ImportFileName)
            Else
                strConn =
                    String.Format(
                        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1;'",
                        ImportFileName)
            End If

            'ファイルを開く
            Dim connExcel As New OleDbConnection(strConn)
            connExcel.Open()

            'ファイルを読取
            Dim schemaTable As Data.DataTable = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            '各シートを取得
            For Each schemaRow As DataRow In schemaTable.Rows
                Dim dt As New Data.DataTable
                Dim strSheetName As String = schemaRow.Item("TABLE_NAME")

                If strSheetName.EndsWith("$") Then
                    Dim query As String = "SELECT * FROM [" & strSheetName & "]"
                    Dim daExcel As New OleDbDataAdapter(query, connExcel)

                    dt.Locale = CultureInfo.CurrentCulture
                    dt.TableName = strSheetName.Replace("$", "")
                    daExcel.Fill(dt)

                    If dt.Rows.Count > 1 Then
                        '第一行を列名に変換する
                        Dim dr As DataRow = dt.Rows(0)

                        For Each dc As DataColumn In dt.Columns

                            Dim strhead = IIf(dr.Item(dc.ColumnName).Equals(DBNull.Value) _
                                              Or dr.Item(dc.ColumnName) Is Nothing, String.Empty,
                                              dr.Item(dc.ColumnName).ToString.Trim)

                            If Not strhead.Equals(String.Empty) Then
                                dc.ColumnName = strhead
                            End If
                        Next

                        dt.Rows.RemoveAt(0)

                        '情報を追加
                        DsExcel.Tables.Add(dt)

                    End If

                End If
            Next

            connExcel.Close()
        End Sub

        ''' <summary>
        '''     データの出力
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <param name="xlsWorkCells"></param>
        ''' <remarks></remarks>
        Public Sub ExportData(dt As Data.DataTable,
                              ByRef xlsWorkCells As Range) Implements IExcelHelper.ExportData
            For intRow = 0 To dt.Rows.Count - 1
                For intColumn = 0 To dt.Columns.Count - 1
                    Dim strColumnName As String = dt.Columns(intColumn).ColumnName

                    'Excelの行番号が"1"からです
                    xlsWorkCells(intRow + 2, intColumn + 1) = dt.Rows(intRow).Item(strColumnName).ToString

                Next
            Next
        End Sub

        ''' <summary>
        '''     タイトルの設定
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <param name="xlsWorkCells"></param>
        ''' <remarks></remarks>
        Public Sub ExportTitle(dt As Data.DataTable,
                               ByRef xlsWorkCells As Range) Implements IExcelHelper.ExportTitle

            For Each dc As DataColumn In dt.Columns
                Dim strColumnTitle As String = dc.Caption

                'タイトルの設定
                xlsWorkCells(1, dt.Columns.IndexOf(dc) + 1) = strColumnTitle

                '編集不可にする
                'CType(xlsWorkCells(1, dt.Columns.IndexOf(dc) + 1), Excel.Range).Locked = False

            Next
        End Sub

        ''' <summary>
        '''     表示スタイル
        ''' </summary>
        ''' <param name="xlsWorkSheet"></param>
        ''' <remarks></remarks>
        Public Sub SetDisplayStyle(dt As Data.DataTable,
                                   ByRef xlsWorkSheet As Worksheet) Implements IExcelHelper.SetDisplayStyle
            'データ範囲
            Dim strRange As String = "A1:" & GetExcelColumnName(dt.Columns.Count) & dt.Rows.Count + 1
            '主キー列
            Dim dcKeys() As DataColumn = dt.PrimaryKey

            'シートの保護
            If SetReadOnlyFlg Then
                xlsWorkSheet.Protect()
            End If

            'タイトルフィルタ
            xlsWorkSheet.Range(strRange).AutoFilter(Field:=1, [Operator]:=XlAutoFilterOperator.xlFilterValues)

            '枠の設定
            If ShowLineFlg Then
                xlsWorkSheet.Range(strRange).Borders.LineStyle = XlLineStyle.xlContinuous
            End If

            '幅の自動調整
            xlsWorkSheet.Columns("A:" & GetExcelColumnName(dt.Columns.Count)).EntireColumn.AutoFit()

            'キーを隠す
            If HideKeysFlg Then
                For Each dcKey As DataColumn In dcKeys
                    Dim intColumnIndex As Integer = dt.Columns.IndexOf(dcKey) + 1
                    Dim strKeyRange As String = GetExcelColumnName(intColumnIndex) & ":" &
                                                GetExcelColumnName(intColumnIndex)

                    xlsWorkSheet.Columns(strKeyRange).EntireColumn.Hidden = HideKeysFlg
                Next
            End If
        End Sub

#End Region

#Region "メソッド(Private)"

        ''' <summary>
        '''     背景色の設定
        ''' </summary>
        ''' <param name="xlsWorkSheet"></param>
        ''' <remarks></remarks>
        Private Sub SetBackColor(ByRef xlsWorkSheet As Worksheet)
            Dim style As Style

            style = xlsWorkSheet.Application.ActiveWorkbook.Styles.Add("InputStyle")
            style.Interior.Color = ColorTranslator.ToOle(BackgroundColor)
        End Sub

        ''' <summary>
        '''     列番号をアルファベットに変換
        ''' </summary>
        ''' <param name="intColumn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function GetExcelColumnName(intColumn As Integer) As String
            Dim dividend As Integer = intColumn
            Dim strColumn As String = String.Empty
            Dim intModulo As Integer

            While dividend > 0
                intModulo = (dividend - 1) Mod 26

                strColumn = Convert.ToChar(65 + intModulo).ToString + strColumn
                dividend = CInt((dividend - intModulo) / 26)
            End While

            Return strColumn
        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     COMオブジェクト開放処理
        ''' </summary>
        ''' <param name="objCom"></param>
        ''' <remarks></remarks>
        ''' ''' ---------------------------------------------------------------------------------------
        Private Shared Sub MrComObject(ByRef objCom As Object)
            Dim intLoopCnt As Integer
            Try
                If objCom IsNot Nothing Then
                    '提供されたランタイム呼び出し可能ラッパーの参照カウントをデクリメントします
                    If Not objCom Is Nothing AndAlso Marshal.IsComObject(objCom) Then
                        Do
                            intLoopCnt = Marshal.ReleaseComObject(objCom)
                        Loop Until intLoopCnt <= 0
                    End If
                End If
            Catch ex As Exception
            Finally
                objCom = Nothing
            End Try
        End Sub

#End Region
    End Class
End Namespace