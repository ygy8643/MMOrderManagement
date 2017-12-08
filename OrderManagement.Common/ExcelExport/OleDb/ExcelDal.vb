Imports System.Text

Namespace ExcelExport.OleDb
    Public Class ExcelDal
        
        '''----------------------------------------------------------------------------------------
        ''' <summary>
        ''' データをファイルに出力する
        ''' </summary>
        ''' <param name="dt">データ</param>
        ''' <remarks></remarks>
        '''----------------------------------------------------------------------------------------
        Public Shared Sub CreateExcel(ByVal da As DataAccess, _
                                      ByVal dt As DataTable, _
                                      ByVal strSheetName As String)

            'シートの作成
            CreateSheet(da, dt, strSheetName)

            'データの出力
            For intRows As Integer = 0 To dt.Rows.Count - 1
                '行ごとにデータを追加
                InsertSheet(da, dt, intRows, strSheetName)
            Next

        End Sub

        ''' <summary>
        ''' Excelシートの作成
        ''' </summary>
        ''' <param name="da"></param>
        ''' <param name="dt"></param>
        ''' <param name="strSheetName"></param>
        ''' <remarks></remarks>
        Private Shared Sub CreateSheet(ByVal da As DataAccess, _
                                       ByVal dt As DataTable, _
                                       ByVal strSheetName As String)
            'シートの作成
            Dim sbCreateSql As New StringBuilder
            sbCreateSql.AppendLine(String.Format("CREATE TABLE [{0}] (", strSheetName))

            Dim strTypeName As String
            For i As Integer = 0 To dt.Columns.Count - 1
                If i > 0 Then
                    sbCreateSql.Append(",")
                End If

                'データ型取得
                Select Case dt.Columns(i).DataType.Name
                    Case GetType(Integer).Name
                        strTypeName = "int"
                    Case GetType(Int16).Name
                        strTypeName = "smallint"
                    Case GetType(Double).Name
                        strTypeName = "float"
                    Case GetType(Boolean).Name
                        strTypeName = "char"
                    Case GetType(String).Name

                        Dim intMaxLength As Integer = dt.Columns(i).MaxLength

                        If intMaxLength < 0 Then
                            strTypeName = "memo"
                        ElseIf intMaxLength <= 255 Then
                            strTypeName = "char"
                        Else
                            strTypeName = "text"
                        End If

                    Case Else
                        strTypeName = dt.Columns(i).DataType.Name
                End Select

                sbCreateSql.AppendLine("[" + dt.Columns(i).Caption + "]" + Space(1) + strTypeName)
            Next

            sbCreateSql.AppendLine(")")

            Using cmd As Data.Common.DbCommand = da.CreateCommand(sbCreateSql.ToString)
                da.ExecuteNonQuery(cmd)
            End Using

        End Sub

        ''' <summary>
        ''' 行ごとにデータを追加
        ''' </summary>
        ''' <param name="da"></param>
        ''' <param name="dt"></param>
        ''' <param name="strSheetName"></param>
        ''' <remarks></remarks>
        Private Shared Sub InsertSheet(ByVal da As DataAccess, _
                                       ByVal dt As DataTable, _
                                       ByVal intRow As Integer, _
                                       ByVal strSheetName As String)

            Dim sbInsertSql As New StringBuilder

            sbInsertSql.AppendLine(String.Format("INSERT INTO [{0}] VALUES (", strSheetName))

            For intColumn As Integer = 0 To dt.Columns.Count - 1
                If intColumn > 0 Then
                    sbInsertSql.Append(",")
                End If

                If dt.Rows(intRow).Item(intColumn).Equals(DBNull.Value) Then
                    sbInsertSql.AppendLine("NULL")
                Else
                    sbInsertSql.AppendLine("'" + dt.Rows(intRow).Item(intColumn).ToString.Replace("'", "''") + "'")
                End If

            Next

            sbInsertSql.AppendLine(")")

            Using cmd As Data.Common.DbCommand = da.CreateCommand(sbInsertSql.ToString)
                da.ExecuteNonQuery(cmd)
            End Using

        End Sub

        '''----------------------------------------------------------------------------------------
        ''' <summary>
        ''' データをテンプレートファイルに出力する
        ''' </summary>
        ''' <param name="dt">出力データ</param>
        ''' <param name="strSheetName">シート名</param>
        ''' <param name="intStartRow">出力開始行番号</param>
        ''' <param name="intStartColumn">出力開始列番号</param>
        ''' <remarks></remarks>
        '''----------------------------------------------------------------------------------------
        Public Shared Sub UpdateExcel(ByVal da As DataAccess, _
                                      ByVal dt As DataTable, _
                                      ByVal strSheetName As String, _
                                      ByVal intStartRow As Integer, _
                                      ByVal intStartColumn As Integer)
            Try
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim sbCreateSql As New StringBuilder

                    '更新文の作成
                    sbCreateSql.Append(CreateUpdateSQL(strSheetName, dt.Rows(i), i + intStartRow, intStartColumn, dt.Columns.Count))

                    Using cmd As Data.Common.DbCommand = da.CreateCommand(sbCreateSql.ToString)
                        da.ExecuteNonQuery(cmd)
                    End Using

                Next

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        ''' <summary>
        ''' セルごとにExcelを更新
        ''' </summary>
        ''' <param name="strSheetName"></param>
        ''' <param name="lstCells"></param>
        ''' <remarks></remarks>
        Public Shared Sub UpdateExcelByCells(ByVal da As DataAccess, _
                                             ByVal strSheetName As String, _
                                             ByVal lstCells As List(Of ExcelCell))
            Try
                For Each cell As ExcelCell In lstCells
                    '更新文の作成
                    Dim sbCreateSql As New StringBuilder
                    Dim strCommand As String = "UPDATE [{0}${1}] SET F1 = '{2}'"
                    Dim strCellNo As String = GetExcelColumnName(cell.ColumnId) & cell.RowId.ToString

                    sbCreateSql.Append(String.Format(strCommand, strSheetName, strCellNo & ":" & strCellNo, cell.Value))

                    Using cmd As Data.Common.DbCommand = da.CreateCommand(sbCreateSql.ToString)
                        da.ExecuteNonQuery(cmd)
                    End Using
                Next

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

#Region "メソッド"

        ''' <summary>
        ''' シート作成SQL文の作成
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <param name="strSheetName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function fncCreateSheet(ByVal dt As DataTable,
                                               ByVal strSheetName As String) As String

            Dim sbSql As New StringBuilder
            sbSql.AppendLine(String.Format("CREATE TABLE [{0}] (", strSheetName))

            Dim strTypeName As String
            For i As Integer = 0 To dt.Columns.Count - 1
                If i > 0 Then
                    sbSql.Append(",")
                End If

                'データ型取得
                Select Case dt.Columns(i).DataType.Name
                    Case GetType(Integer).Name
                        strTypeName = "int"
                    Case GetType(Int16).Name
                        strTypeName = "smallint"
                    Case GetType(Double).Name
                        strTypeName = "float"
                    Case GetType(Boolean).Name
                        strTypeName = "char"
                    Case GetType(String).Name

                        Dim intMaxLength As Integer = dt.Columns(i).MaxLength

                        If intMaxLength < 0 Then
                            strTypeName = "memo"
                        ElseIf intMaxLength <= 255 Then
                            strTypeName = "char"
                        Else
                            strTypeName = "text"
                        End If

                    Case Else
                        strTypeName = dt.Columns(i).DataType.Name
                End Select

                sbSql.AppendLine("[" + dt.Columns(i).Caption + "]" + Space(1) + strTypeName)
            Next

            sbSql.AppendLine(")")

            Return sbSql.ToString
        End Function

        ''' <summary>
        ''' データ出力SQL文の作成
        ''' </summary>
        ''' <param name="inti"></param>
        ''' <param name="dt"></param>
        ''' <param name="strSheetName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function fncOutputData(ByVal inti As Integer, _
                                       ByVal dt As DataTable, _
                                       ByVal strSheetName As String) As String

            Dim sbSql As New StringBuilder

            sbSql.AppendLine(String.Format("INSERT INTO [{0}] VALUES (", strSheetName))

            For j As Integer = 0 To dt.Columns.Count - 1
                If j > 0 Then
                    sbSql.Append(",")
                End If

                If dt.Rows(inti).Item(j).Equals(DBNull.Value) Then
                    sbSql.AppendLine("NULL")
                Else
                    sbSql.AppendLine("'" + dt.Rows(inti).Item(j).ToString.Replace("'", "''") + "'")
                End If
            Next

            sbSql.AppendLine(")")

            Return sbSql.ToString
        End Function

        ''' <summary>
        ''' 更新SQL文の作成
        ''' </summary>
        ''' <param name="strSheetName">シート名</param>
        ''' <param name="dr">データ</param>
        ''' <param name="intRowID">行番号</param>
        ''' <param name="intStartColumn">出力開始列番号</param>
        ''' <param name="intColumnCount">列数</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CreateUpdateSQL(ByVal strSheetName As String, _
                                                ByVal dr As DataRow, _
                                                ByVal intRowID As Integer, _
                                                ByVal intStartColumn As Integer, _
                                                ByVal intColumnCount As Integer) As String
            Dim strResult As String = " UPDATE [{0}${1}] SET {2}"
            Dim strRange As String = String.Empty
            Dim strStart As String = String.Empty
            Dim strEnd As String = String.Empty
            Dim strUpdColumn As String = String.Empty

            '開始範囲
            strStart = GetExcelColumnName(intStartColumn) & intRowID.ToString
            '終了範囲
            strEnd = GetExcelColumnName(intStartColumn + intColumnCount - 1) & intRowID.ToString
            '総範囲
            strRange = strStart & ":" & strEnd

            '出力データ
            For inti As Integer = 0 To intColumnCount - 1
                strUpdColumn &= "F" & (inti + 1) & " = "

                If dr.Item(inti).Equals(DBNull.Value) Then
                    strUpdColumn &= "NULL"
                Else
                    strUpdColumn &= "'" + dr.Item(inti).ToString.Replace("'", "''") & "'"
                End If

                If Not inti = intColumnCount - 1 Then
                    strUpdColumn &= ", "
                End If
            Next

            strResult = String.Format(strResult, strSheetName, strRange, strUpdColumn)

            Return strResult

        End Function

        ''' <summary>
        ''' 削除SQL文の作成
        ''' </summary>
        ''' <param name="strSheetName">シート名</param>
        ''' <param name="intColumnNum">列数</param>
        ''' <param name="intRowID">行番号</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CreateDeleteSQL(ByVal strSheetName As String, _
                                                ByVal intColumnNum As Integer, _
                                                ByVal intRowID As Integer) As String
            Dim strResult As String = " UPDATE [{0}${1}] SET {2}"
            Dim strRange As String = String.Empty
            Dim strStart As String = String.Empty
            Dim strEnd As String = String.Empty
            Dim strUpdColumn As String = String.Empty

            '開始範囲
            strStart = "A" & intRowID.ToString
            '終了範囲
            strEnd = GetExcelColumnName(intColumnNum) & intRowID.ToString
            '総範囲
            strRange = strStart & ":" & strEnd

            '出力データ
            For inti As Integer = 1 To intColumnNum
                strUpdColumn &= "F" & inti & " = "
                strUpdColumn &= "NULL"


                If Not inti = intColumnNum Then
                    strUpdColumn &= ", "
                End If
            Next

            strResult = String.Format(strResult, strSheetName, strRange, strUpdColumn)

            Return strResult

        End Function

        ''' <summary>
        ''' 列番号をアルファベットに変換
        ''' </summary>
        ''' <param name="intColumn"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function GetExcelColumnName(ByVal intColumn As Integer) As String
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

#End Region

    End Class
End NameSpace