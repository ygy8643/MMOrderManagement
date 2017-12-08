Imports System.Configuration
Imports System.Data.Common

Namespace ExcelExport.OleDb
    Public Class DataAccess
        Implements IDisposable

#Region "定数"

        '接続DB種類
        Public Const ProviderOracle As String = "Oracle.DataAccess.Client"     'オラクル
        Public Const ProviderSqlserver As String = "System.Data.SqlClient"     'SqlServer
        Public Const ProviderOle As String = "System.Data.OleDb"               'アクセス等

        Private Const CCommandTimeOut As Int32 = 120                           'コマンドオブジェクトのデフォルトタイムアウト値

#End Region

#Region "変数"

        Private _mConnectionString As String         'DB接続文字列
        Private _mConnection As DbConnection         'コネクション
        Private _mTransaction As DbTransaction       'トランザクション
        Private ReadOnly _mFactory As DbProviderFactory       'DBファクトリー

#End Region

#Region "コンストラクタ"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     コンストラクタ
        ''' </summary>
        ''' <param name="strProviderName">
        '''     Provider_Oracle or Provider_Sqlserver or Provider_Ole
        ''' </param>
        ''' -----------------------------------------------------------------------------------------
        Public Sub New(strProviderName As String)
            _mFactory = DbProviderFactories.GetFactory(strProviderName)
            _mConnection = Nothing
            _mTransaction = Nothing
            _mConnectionString = ""
        End Sub

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     コンストラクタ
        ''' </summary>
        ''' <param name="connectionSettings"></param>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Sub New(connectionSettings As ConnectionStringSettings)

            Me.New(connectionSettings.ConnectionString, connectionSettings.ProviderName)
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     コンストラクタ
        ''' </summary>
        ''' <param name="strConnectionString">
        '''     接続文字列
        ''' </param>
        ''' <param name="strProviderName">
        '''     Provider_Oracle or Provider_Sqlserver or Provider_Ole
        ''' </param>
        ''' -----------------------------------------------------------------------------------------
        Public Sub New(strConnectionString As String, strProviderName As String)
            _mFactory = DbProviderFactories.GetFactory(strProviderName)
            _mConnection = Nothing
            _mTransaction = Nothing
            _mConnectionString = strConnectionString

            OpenConnection()
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     Dispose
        ''' </summary>
        ''' -----------------------------------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            'コネクションのクローズ
            CloseConnection()

            GC.SuppressFinalize(Me)
        End Sub

#End Region

#Region "プロパティ"

#End Region

#Region "メソッド"

#Region "接続関連"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     データベースに接続します。
        ''' </summary>
        ''' -----------------------------------------------------------------------------------------
        Public Sub OpenConnection()
            CloseConnection()
            _mConnection = _mFactory.CreateConnection
            _mConnection.ConnectionString = _mConnectionString
            _mConnection.Open()
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     データベースに接続します。
        ''' </summary>
        ''' <param name="csbuilder">
        '''     接続先を指定済みのDbConnectionStringBuilder
        ''' </param>
        ''' -----------------------------------------------------------------------------------------
        Public Sub OpenConnection(csbuilder As DbConnectionStringBuilder)
            _mConnectionString = csbuilder.ConnectionString
            OpenConnection()
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     データベースに接続します。
        ''' </summary>
        ''' <param name="connectionString">
        '''     接続先文字列
        ''' </param>
        ''' -----------------------------------------------------------------------------------------
        Public Sub OpenConnection(connectionString As String)
            _mConnectionString = ConnectionString
            OpenConnection()
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     データベースの接続を閉じます。
        ''' </summary>
        ''' -----------------------------------------------------------------------------------------
        Public Sub CloseConnection()
            If Not (_mConnection Is Nothing) Then
                CommitTransaction()
                _mConnection.Close()
                _mConnection.Dispose()
                _mConnection = Nothing
            End If
        End Sub

#End Region

#Region "トランザクション関連"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     トランザクションを開始します。
        ''' </summary>
        ''' -----------------------------------------------------------------------------------------
        Public Sub BeginTransaction(Optional level As IsolationLevel = IsolationLevel.ReadCommitted)
            If Not (_mConnection Is Nothing) Then
                _mTransaction = _mConnection.BeginTransaction(Level)
            End If
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     トランザクションをコミットします。
        ''' </summary>
        ''' -----------------------------------------------------------------------------------------
        Public Sub CommitTransaction()
            If Not (_mTransaction Is Nothing) Then
                _mTransaction.Commit()
                _mTransaction.Dispose()
                _mTransaction = Nothing
            End If
        End Sub

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     トランザクションをロールバックします。
        ''' </summary>
        ''' -----------------------------------------------------------------------------------------
        Public Sub RollbackTransaction()
            If Not (_mTransaction Is Nothing) Then
                _mTransaction.Rollback()
                _mTransaction.Dispose()
                _mTransaction = Nothing
            End If
        End Sub

#End Region

#Region "パラメータ関連"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <returns>
        '''     DBParameter
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter() As DbParameter
            Return _mFactory.CreateParameter()
        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <param name="ParamName">パラメータ名</param>
        ''' <param name="value">パラメータ値</param>
        ''' <param name="direction">
        '''     入力専用、出力専用、双方向、またはストアド プロシージャの戻り値パラメータ
        ''' </param>
        ''' <param name="strSourceColumn">
        '''     読み込みまたは戻しに使用される列　アダプタを使用した追加・更新・削除の場合必須
        ''' </param>
        ''' <param name="drVersion">バージョン　アダプタを使用した更新・削除の場合必須</param>
        ''' <returns>DBParameter</returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter(
                                                  paramName As String,
                                                  value As String,
                                                  Optional direction As ParameterDirection = ParameterDirection.Input,
                                                  Optional strSourceColumn As String = "",
                                                  Optional drVersion As DataRowVersion = DataRowVersion.Current
                                                  ) As DbParameter

            Return CreateParameter(
                paramName,
                IIf(IsEmpty(value), DBNull.Value, value),
                DbType.AnsiString,
                Direction,
                strSourceColumn,
                drVersion
                )
        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <param name="ParamName">パラメータ名</param>
        ''' <param name="value">パラメータ値</param>
        ''' <param name="direction">
        '''     入力専用、出力専用、双方向、またはストアド プロシージャの戻り値パラメータ
        ''' </param>
        ''' <param name="strSourceColumn">
        '''     読み込みまたは戻しに使用される列　アダプタを使用した追加・更新・削除の場合必須
        ''' </param>
        ''' <param name="drVersion">バージョン　アダプタを使用した更新・削除の場合必須</param>
        ''' <returns>DBParameter</returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter(
                                                  paramName As String,
                                                  value As Decimal,
                                                  Optional direction As ParameterDirection = ParameterDirection.Input,
                                                  Optional strSourceColumn As String = "",
                                                  Optional drVersion As DataRowVersion = DataRowVersion.Current
                                                  ) As DbParameter

            Return CreateParameter(
                paramName,
                value,
                DbType.Decimal,
                Direction,
                strSourceColumn,
                drVersion
                )
        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <param name="ParamName">パラメータ名</param>
        ''' <param name="value">パラメータ値</param>
        ''' <param name="direction">
        '''     入力専用、出力専用、双方向、またはストアド プロシージャの戻り値パラメータ
        ''' </param>
        ''' <param name="strSourceColumn">
        '''     読み込みまたは戻しに使用される列　アダプタを使用した追加・更新・削除の場合必須
        ''' </param>
        ''' <param name="drVersion">バージョン　アダプタを使用した更新・削除の場合必須</param>
        ''' <returns>DBParameter</returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter(
                                                  paramName As String,
                                                  value As Boolean,
                                                  Optional direction As ParameterDirection = ParameterDirection.Input,
                                                  Optional strSourceColumn As String = "",
                                                  Optional drVersion As DataRowVersion = DataRowVersion.Current
                                                  ) As DbParameter

            Return CreateParameter(
                paramName,
                value,
                DbType.Boolean,
                Direction,
                strSourceColumn,
                drVersion
                )
        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <param name="ParamName">パラメータ名</param>
        ''' <param name="value">パラメータ値</param>
        ''' <param name="direction">
        '''     入力専用、出力専用、双方向、またはストアド プロシージャの戻り値パラメータ
        ''' </param>
        ''' <param name="strSourceColumn">
        '''     読み込みまたは戻しに使用される列　アダプタを使用した追加・更新・削除の場合必須
        ''' </param>
        ''' <param name="drVersion">バージョン　アダプタを使用した更新・削除の場合必須</param>
        ''' <returns>DBParameter</returns>
        ''' <remarks></remarks>
        ''' ---------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter(
                                                  paramName As String,
                                                  value As Nullable(Of DateTime),
                                                  Optional direction As ParameterDirection = ParameterDirection.Input,
                                                  Optional strSourceColumn As String = "",
                                                  Optional drVersion As DataRowVersion = DataRowVersion.Current
                                                  ) As DbParameter

            Return CreateParameter(
                paramName,
                IIf(value.HasValue, value, DBNull.Value),
                DbType.DateTime,
                Direction,
                strSourceColumn,
                drVersion
                )
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <param name="paramName">
        '''     パラメータ名
        ''' </param>
        ''' <param name="value">
        '''     パラメータ値
        ''' </param>
        ''' <param name="type">
        '''     パラメータタイプ
        ''' </param>
        ''' <param name="direction">
        '''     入力専用、出力専用、双方向、またはストアド プロシージャの戻り値パラメータ
        ''' </param>
        ''' <param name="strSourceColumn">
        '''     読み込みまたは戻しに使用される列　アダプタを使用した追加・更新・削除の場合必須
        ''' </param>
        ''' <param name="drVersion">
        '''     バージョン　アダプタを使用した更新・削除の場合必須
        ''' </param>
        ''' <returns>
        '''     DBParameter
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter(
                                                  paramName As String,
                                                  value As Object,
                                                  type As DbType,
                                                  Optional direction As ParameterDirection = ParameterDirection.Input,
                                                  Optional strSourceColumn As String = "",
                                                  Optional drVersion As DataRowVersion = DataRowVersion.Current
                                                  ) As DbParameter

            Dim param As DbParameter = _mFactory.CreateParameter()
            param.DbType = Type
            param.ParameterName = ParamName
            param.Value = Value
            param.Direction = Direction
            param.SourceColumn = strSourceColumn
            param.SourceVersion = drVersion
            Return param
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBParameterを取得します。
        ''' </summary>
        ''' <param name="paramName">
        '''     パラメータ名
        ''' </param>
        ''' <param name="value">
        '''     パラメータ値
        ''' </param>
        ''' <param name="type">
        '''     パラメータタイプ
        ''' </param>
        ''' <param name="direction">
        '''     入力専用、出力専用、双方向、またはストアド プロシージャの戻り値パラメータ
        ''' </param>
        ''' <param name="strSourceColumn">
        '''     読み込みまたは戻しに使用される列　アダプタを使用した追加・更新・削除の場合必須
        ''' </param>
        ''' <param name="drVersion">
        '''     バージョン　アダプタを使用した更新・削除の場合必須
        ''' </param>
        ''' <returns>
        '''     DBParameter
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Overloads Function CreateParameter(
                                                  paramName As String,
                                                  value As Object,
                                                  type As DbType,
                                                  size As Int64,
                                                  Optional direction As ParameterDirection = ParameterDirection.Input,
                                                  Optional strSourceColumn As String = "",
                                                  Optional drVersion As DataRowVersion = DataRowVersion.Current
                                                  ) As DbParameter

            Dim param As DbParameter = _mFactory.CreateParameter()
            param.DbType = Type
            param.Size = Size
            param.ParameterName = ParamName
            param.Value = Value
            param.Direction = Direction
            param.SourceColumn = strSourceColumn
            param.SourceVersion = drVersion
            Return param
        End Function

#End Region

#Region "コマンド関連"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBCommandを取得します。
        ''' </summary>
        ''' <returns>
        '''     DBCommand
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function CreateCommand() As DbCommand
            Dim cmd As DbCommand = _mConnection.CreateCommand()
            cmd.CommandTimeout = CCommandTimeOut
            cmd.CommandText = String.Empty
            If Not (_mTransaction Is Nothing) Then
                cmd.Transaction = _mTransaction
            End If

            Return cmd
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBCommandを取得します。
        ''' </summary>
        ''' <param name="comType">
        '''     コマンドオブジェクトタイプ
        ''' </param>
        ''' <returns>
        '''     DBCommand
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function CreateCommand(comType As CommandType) As DbCommand
            Dim cmd As DbCommand = _mConnection.CreateCommand()
            cmd.CommandTimeout = CCommandTimeOut
            cmd.CommandText = String.Empty
            cmd.CommandType = ComType
            If Not (_mTransaction Is Nothing) Then
                cmd.Transaction = _mTransaction
            End If

            Return cmd
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DBCommandを取得します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="comType">
        '''     コマンドオブジェクトタイプ
        ''' </param>
        ''' <returns>
        '''     DBCommand
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function CreateCommand(strSql As String, Optional comType As CommandType = CommandType.Text) As DbCommand
            Dim cmd As DbCommand = _mConnection.CreateCommand()
            cmd.CommandTimeout = CCommandTimeOut
            cmd.CommandText = strSql
            cmd.CommandType = ComType
            If Not (_mTransaction Is Nothing) Then
                cmd.Transaction = _mTransaction
            End If

            Return cmd
        End Function

#End Region

#Region "SQL文実行関連"

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDbDataReaderで取得します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <returns>
        '''     DbDataReader
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteReader(strSql As String) As DbDataReader
            Dim cmd As DbCommand = CreateCommand(strSql)
            Return ExecuteReader(cmd)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDbDataReaderで取得します。
        ''' </summary>
        ''' <param name="cmd">
        '''     DbCommandオブジェクト
        ''' </param>
        ''' <returns>
        '''     DbDataReader
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteReader(cmd As DbCommand) As DbDataReader
            Dim drd As DbDataReader = Nothing

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then

                cmd.Connection = _mConnection
                If Not (_mTransaction Is Nothing) Then
                    cmd.Transaction = _mTransaction
                End If

                drd = cmd.ExecuteReader
            End If

            Return drd
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDataSetで取得します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     DataTable
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteQuery(strSql As String, Optional intTimeOut As Int32 = - 1) As DataTable
            Dim cmd As DbCommand = CreateCommand(strSql)
            Return ExecuteQuery(cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDataSetで取得します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="comType">
        '''     コマンドタイプ
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     DataTable
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteQuery(strSql As String, comType As CommandType, Optional intTimeOut As Int32 = - 1) _
            As DataTable
            Dim cmd As DbCommand = CreateCommand(strSql, ComType)
            Return ExecuteQuery(cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDataSetで取得します。
        ''' </summary>
        ''' <param name="cmd">
        '''     DbCommandオブジェクト
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     DataTable
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteQuery(cmd As DbCommand, Optional intTimeOut As Int32 = - 1) As DataTable
            Dim adapter As DbDataAdapter = _mFactory.CreateDataAdapter()
            Dim dt As DataTable = Nothing

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then

                cmd.Connection = _mConnection
                If Not (_mTransaction Is Nothing) Then
                    cmd.Transaction = _mTransaction
                End If
                If intTimeOut <> - 1 Then
                    cmd.CommandTimeout = intTimeOut
                End If

                adapter.SelectCommand = cmd
                dt = New DataTable
                adapter.Fill(dt)
            End If

            Return dt
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDataSetで取得します。
        ''' </summary>
        ''' <param name="dt">
        '''     結果格納用データテーブル
        ''' </param>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     レコード件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteQuery(ByRef dt As DataTable, strSql As String, Optional intTimeOut As Int32 = - 1) _
            As Int32
            Dim cmd As DbCommand = CreateCommand(strSql)
            Return ExecuteQuery(dt, cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDataSetで取得します。
        ''' </summary>
        ''' <param name="dt">
        '''     結果格納用データテーブル
        ''' </param>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="comType">
        '''     コマンドタイプ
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     レコード件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteQuery(ByRef dt As DataTable, strSql As String, comType As CommandType,
                                     Optional intTimeOut As Int32 = - 1) As Int32
            Dim cmd As DbCommand = CreateCommand(strSql, ComType)
            Return ExecuteQuery(dt, cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     SELECT文を実行し、結果をDataSetで取得します。
        ''' </summary>
        ''' <param name="dt">
        '''     結果格納用データテーブル
        ''' </param>
        ''' <param name="cmd">
        '''     DbCommandオブジェクト
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     レコード件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteQuery(ByRef dt As DataTable, cmd As DbCommand, Optional intTimeOut As Int32 = - 1) _
            As Int32
            ExecuteQuery = 0
            Dim adapter As DbDataAdapter = _mFactory.CreateDataAdapter()

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then

                cmd.Connection = _mConnection
                If Not (_mTransaction Is Nothing) Then
                    cmd.Transaction = _mTransaction

                End If
                If intTimeOut <> - 1 Then
                    cmd.CommandTimeout = intTimeOut
                End If

                adapter.SelectCommand = cmd
                If dt Is Nothing Then
                    dt = New DataTable
                End If
                ExecuteQuery = adapter.Fill(dt)
            End If
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     単一の値を返すSELECT文を実行し、結果を取得します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <returns>
        '''     値
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteScalar(strSql As String) As Object
            Dim cmd As DbCommand = CreateCommand(strSql)
            Return ExecuteScalar(cmd)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     単一の値を返すSELECT文を実行し、結果を取得します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="comType">
        '''     コマンドタイプ
        ''' </param>
        ''' <returns>
        '''     値
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteScalar(strSql As String, comType As CommandType) As Object
            Dim cmd As DbCommand = CreateCommand(strSql, ComType)
            Return ExecuteScalar(cmd)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     単一の値を返すSELECT文を実行し、結果を取得します。
        ''' </summary>
        ''' <param name="cmd">
        '''     DbCommandオブジェクト
        ''' </param>
        ''' <returns>
        '''     値
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteScalar(cmd As DbCommand) As Object
            Dim ret As Object = Nothing

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then

                cmd.Connection = _mConnection
                If Not (_mTransaction Is Nothing) Then
                    cmd.Transaction = _mTransaction
                End If

                ret = cmd.ExecuteScalar()
            End If

            Return ret
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     INSERT, UPDATE, DELETEなどの更新SQLを実行します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     更新件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteNonQuery(strSql As String, Optional intTimeOut As Int32 = - 1) As Integer
            Dim cmd As DbCommand = CreateCommand(strSql)
            Return ExecuteNonQuery(cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     INSERT, UPDATE, DELETEなどの更新SQLを実行します。
        ''' </summary>
        ''' <param name="strSql">
        '''     SQL文
        ''' </param>
        ''' <param name="comType">
        '''     コマンドタイプ
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     更新件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteNonQuery(strSql As String, comType As CommandType, Optional intTimeOut As Int32 = - 1) _
            As Integer
            Dim cmd As DbCommand = CreateCommand(strSql, ComType)
            Return ExecuteNonQuery(cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     INSERT, UPDATE, DELETEなどの更新SQLを実行します。
        ''' </summary>
        ''' <param name="cmd">
        '''     DbCommandオブジェクト
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     更新件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function ExecuteNonQuery(cmd As DbCommand, Optional intTimeOut As Int32 = - 1) As Integer
            Dim count = 0

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then

                cmd.Connection = _mConnection
                If Not (_mTransaction Is Nothing) Then
                    cmd.Transaction = _mTransaction
                End If
                If intTimeOut <> - 1 Then
                    cmd.CommandTimeout = intTimeOut
                End If

                count = cmd.ExecuteNonQuery()
            End If
            Return count
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     指定したデータセットをアダプタを使用して更新します。
        '''     Selectに対応したUpdate、Insert、Deleteが作成されます。
        '''     以下の条件以外は例外が発生します。
        '''     クエリがただ一つのテーブルからデータを取得するようになっていること。
        '''     そのテーブルに主キーが設定されていること。
        '''     その主キーのカラムがクエリに設定されていること。
        ''' </summary>
        ''' <param name="dt">
        '''     データテーブル
        ''' </param>
        ''' <param name="strSql">
        '''     Select文
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     更新件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function Update(ByRef dt As DataTable, strSql As String, Optional intTimeOut As Int32 = - 1) As Integer
            Dim cmd As DbCommand = CreateCommand(strSql)
            Return Update(dt, cmd, intTimeOut)
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     指定したデータセットをアダプタを使用して更新します。
        '''     Selectに対応したUpdate、Insert、Deleteが作成されます。
        '''     以下の条件以外は例外が発生します。
        '''     クエリがただ一つのテーブルからデータを取得するようになっていること。
        '''     そのテーブルに主キーが設定されていること。
        '''     その主キーのカラムがクエリに設定されていること。
        ''' </summary>
        ''' <param name="dt">
        '''     データテーブル
        ''' </param>
        ''' <param name="cmdSelect">
        '''     Select用DbCommandオブジェクト
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        '''     指定しない場合は、Select用DbCommandオブジェクトの値が使用されます。
        ''' </param>
        ''' <returns>
        '''     更新件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function Update(ByRef dt As DataTable, cmdSelect As DbCommand, Optional intTimeOut As Int32 = - 1) _
            As Integer
            Dim count = 0

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then

                cmdSelect.Connection = _mConnection
                If Not (_mTransaction Is Nothing) Then
                    cmdSelect.Transaction = _mTransaction
                End If
                If intTimeOut <> - 1 Then
                    cmdSelect.CommandTimeout = intTimeOut
                End If

                Using adapter As DbDataAdapter = _mFactory.CreateDataAdapter()
                    'Selectコマンドのみセット
                    adapter.SelectCommand = cmdSelect

                    'コマンドビルダーとデータアダプタを関連付け
                    'コマンドビルダーによって自動的にInsert、Update、Deleteを作成される
                    Dim cb As DbCommandBuilder = _mFactory.CreateCommandBuilder()
                    cb.DataAdapter = adapter

                    ''自動生成されたコマンドの確認
                    'System.Diagnostics.Debug.WriteLine(cb.GetUpdateCommand(True).CommandText)
                    'System.Diagnostics.Debug.WriteLine(cb.GetInsertCommand(True).CommandText)
                    'System.Diagnostics.Debug.WriteLine(cb.GetDeleteCommand(True).CommandText)

                    '更新
                    count = adapter.Update(dt)
                End Using
            End If
            Return count
        End Function

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     指定したデータセットをアダプタを使用して更新します。
        ''' </summary>
        ''' <param name="dt">
        '''     データテーブル
        ''' </param>
        ''' <param name="cmdUpdate">
        '''     Update用DbCommandオブジェクト
        ''' </param>
        ''' <param name="cmdInsert">
        '''     Insert用DbCommandオブジェクト
        ''' </param>
        ''' <param name="cmdDelete">
        '''     Delete用DbCommandオブジェクト
        ''' </param>
        ''' <param name="intTimeOut">
        '''     タイムアウト値（秒）
        ''' </param>
        ''' <returns>
        '''     更新件数
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function Update(ByRef dt As DataTable, cmdUpdate As DbCommand, cmdInsert As DbCommand,
                               cmdDelete As DbCommand, Optional intTimeOut As Int32 = - 1) As Integer
            Dim count = 0

            'コネクションが確立していない場合はSQLを実行しない
            If Not (_mConnection Is Nothing) Then
                cmdUpdate.Connection = _mConnection
                cmdInsert.Connection = _mConnection
                cmdDelete.Connection = _mConnection

                If Not (_mTransaction Is Nothing) Then
                    cmdUpdate.Transaction = _mTransaction
                    cmdInsert.Transaction = _mTransaction
                    cmdDelete.Transaction = _mTransaction
                End If

                If intTimeOut <> - 1 Then
                    cmdUpdate.CommandTimeout = intTimeOut
                    cmdInsert.CommandTimeout = intTimeOut
                    cmdDelete.CommandTimeout = intTimeOut
                End If

                Using adapter As DbDataAdapter = _mFactory.CreateDataAdapter()
                    adapter.UpdateCommand = cmdUpdate
                    adapter.InsertCommand = cmdInsert
                    adapter.DeleteCommand = cmdDelete

                    '更新
                    count = adapter.Update(dt)
                End Using
            End If
            Return count
        End Function

#End Region

        ''' -----------------------------------------------------------------------------------------
        ''' <summary>
        '''     DbConnectionStringBuilderを取得します。
        ''' </summary>
        ''' <returns>
        '''     DbConnectionStringBuilder
        ''' </returns>
        ''' -----------------------------------------------------------------------------------------
        Public Function CreateConnectionStringBuilder() As DbConnectionStringBuilder
            Return _mFactory.CreateConnectionStringBuilder()
        End Function

        ''' ---------------------------------------------------------------------------------------
        ''' <summary>
        '''     空文字かどうかを判定する
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>空白のみの場合は空文字として扱う</remarks>
        ''' ---------------------------------------------------------------------------------------
        Private Function IsEmpty(value As String) As Boolean

            If value IsNot Nothing Then
                value = value.Trim
            End If

            If String.IsNullOrEmpty(value) Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region
    End Class
End NameSpace