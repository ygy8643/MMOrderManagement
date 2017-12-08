Imports System.Configuration
Imports System.Data.Common

Namespace ExcelExport.OleDb
    Public Class DbMethods
        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     SQLServerDB接続済みのデータアクセスクラスを取得する
        ''' </summary>
        ''' <param name="strConnect">接続文字列</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Shared Function CreateSqlServerDbAccess(strConnect As String) As DataAccess

            Dim strCnn As String = GetSetting(strConnect)

            Dim strProvider As String = DataAccess.ProviderSqlserver

            Return New DataAccess(strCnn, strProvider)
        End Function

        ''' <summary>
        '''     OracleDB接続済みのデータアクセスクラスを取得する
        ''' </summary>
        ''' <param name="strConnect"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CreateOracleDbAccess(strConnect As String) As DataAccess

            Dim strCnn As String = GetSetting(strConnect)

            Dim strProvider As String = DataAccess.ProviderOracle

            Return New DataAccess(strCnn, strProvider)
        End Function

        ''' <summary>
        '''     OleDB接続済みのデータアクセスクラスを取得する
        ''' </summary>
        ''' <param name="strConnect"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CreateOleDbAccess(strConnect As String,
                                                 strFilePath As String) As DataAccess

            Dim strProvider As String = DataAccess.ProviderOle
            Dim sbCnn As New DbConnectionStringBuilder

            sbCnn.ConnectionString = GetSetting(strConnect)
            sbCnn("Data Source") = strFilePath

            Return New DataAccess(sbCnn.ConnectionString, strProvider)
        End Function

        ''' <summary>
        '''     OleDB接続済みのデータアクセスクラスを取得する
        ''' </summary>
        ''' <param name="strConnect">接続文字列名</param>
        ''' <param name="strFilePath">出力ファイルパス</param>
        ''' <param name="blnHdr">第1行がタイトルかどうか</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CreateOleDbAccess(strConnect As String,
                                                 strFilePath As String,
                                                 blnHdr As Boolean) As DataAccess

            Dim strProvider As String = DataAccess.ProviderOle
            Dim sbCnn As New DbConnectionStringBuilder

            If Not blnHDR Then
                'Excelにタイトルが含まれない場合
                sbCnn.ConnectionString = GetSetting(strConnect).Replace("HDR=YES", "HDR=NO")
            Else
                sbCnn.ConnectionString = GetSetting(strConnect)
            End If

            sbCnn("Data Source") = strFilePath

            Return New DataAccess(sbCnn.ConnectionString, strProvider)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     app.configファイルから取得
        ''' </summary>
        ''' <param name="strKey">検索KEY</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' -----------------------------------------------------------------------------------------
        Public Shared Function GetSetting(strKey As String) As String
            Dim ret = ""
            Try
                Dim settings As ConnectionStringSettings

                'app.configファイルから取得
                settings = ConfigurationManager.ConnectionStrings(strKey)

                If settings IsNot Nothing Then
                    '接続文字列の設定
                    ret = settings.ConnectionString
                End If

            Catch ex As Exception
                ret = ""
            End Try

            Return ret
        End Function
    End Class
End NameSpace