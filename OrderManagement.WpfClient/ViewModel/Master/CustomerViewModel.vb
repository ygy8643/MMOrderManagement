Imports System.Data
Imports MahApps.Metro.Controls.Dialogs
Imports Microsoft.Win32
Imports OrderManagement.Client.Entities
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common.ExcelExport.Interop
Imports OrderManagement.WpfClient.Service

Namespace ViewModel.Master
    Public Class CustomerViewModel
        Inherits BaseMasterViewModel(Of CustomerClient)

#Region "フィールド"

        ''' <summary>
        '''     サービス
        ''' </summary>
        Private ReadOnly _customerService As ICustomerServiceAgent

        ''' <summary>
        '''     ダイアログ
        ''' </summary>
        Private ReadOnly _dialogCoordinator As IDialogCoordinator

        ''' <summary>
        '''     エラー件数
        ''' </summary>
        Public Shared Property ErrorCount As Integer

#End Region

#Region "プロパティ"

        ''' <summary>
        '''     タイトル
        ''' </summary>
        Public ReadOnly Property Title As String
            Get
                Return "用户信息"
            End Get
        End Property

        ''' <summary>
        '''     選択したデータ
        ''' </summary>
        ''' <remarks></remarks>
        Private _selectedData As CustomerClient

        Public Property SelectedData As CustomerClient
            Get
                Return _selectedData
            End Get
            Set
                [Set]("SelectedData", _selectedData, Value)

                If Value Is Nothing OrElse
                   String.IsNullOrEmpty(Value.CustomerId) OrElse Value.CustomerId = 0 Then
                    DetailData = New CustomerClient
                Else

                    'リストからデータを選択する時には修正モードに切り替え、詳細をセット
                    IsCreateMaster = False
                    DetailData = New CustomerClient With {
                        .CustomerId = Value.CustomerId,
                        .Name = Value.Name,
                        .Address = Value.Address,
                        .PostCode = Value.PostCode,
                        .Phone = Value.Phone,
                        .WechatName = Value.WechatName,
                        .TaobaoName = Value.TaobaoName}
                End If
            End Set
        End Property

#End Region

#Region "コンストラクター"

        ''' <summary>
        '''     初期化
        ''' </summary>
        ''' <param name="dataService"></param>
        ''' <remarks></remarks>
        Public Sub New(dataService As ICustomerServiceAgent, dialogInstance As IDialogCoordinator)

            MyBase.New()

            SelectedData = New CustomerClient()
            SearchCondition = New CustomerClient()

            'サービスの初期化
            _customerService = dataService
            _dialogCoordinator = dialogInstance

            'リストの初期化
        End Sub

#End Region

        ''' <summary>
        '''     追加
        ''' </summary>
        Public Overrides Sub AddMasterData()

            Try
                '存在チェック
                If _customerService.CustomerExists(DetailData.CustomerId) Then

                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "データが重複しました")
                Else

                    '新規追加
                    Dim result = _customerService.CreateCustomer(DetailData)

                    If result.IsSuccess Then
                        'Show Success Message and add the data to list
                        _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "追加しました")

                        '結果リストをリセット
                        SearchMasterData()

                    Else
                        _dialogCoordinator.ShowMessageAsync(Me, "エラー", "追加失敗しました")

                    End If
                End If

            Catch ex As Exception
                Log.Error(ex.Message & ex.StackTrace)
            End Try
        End Sub

        ''' <summary>
        '''     クリア
        ''' </summary>
        Public Overrides Sub ClearDetail()
            '新規モードにセット
            IsCreateMaster = True

            '詳細をクリア
            DetailData = New CustomerClient()
            SelectedData = New CustomerClient()
        End Sub

        ''' <summary>
        '''     削除
        ''' </summary>
        Public Overrides Sub DeleteMasterData()
            Try

                '削除
                Dim result = _customerService.DeleteCustomer(DetailData.CustomerId)

                If result.IsSuccess Then

                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "削除しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New CustomerClient()
                    IsCreateMaster = True
                Else
                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "削除失敗しました")

                End If

            Catch ex As Exception
                Log.Error(ex.Message & ex.StackTrace)
            End Try
        End Sub

        ''' <summary>
        '''     メッセージを隠す
        ''' </summary>
        Public Overrides Sub HideSuccessMessage()
            IsShowResultFlag = False
        End Sub

        ''' <summary>
        '''     ロード
        ''' </summary>
        Public Overrides Sub LoadMasterData()
            Throw New NotImplementedException()
        End Sub

        ''' <summary>
        '''     検索
        ''' </summary>
        Public Overrides Sub SearchMasterData()
            Try
                '検索
                MasterData = _customerService.GetCustomerByCondition(SearchCondition)

            Catch ex As Exception
                Log.Error(ex.Message & ex.StackTrace)
            End Try
        End Sub

        ''' <summary>
        '''     更新
        ''' </summary>
        Public Overrides Sub UpdateMasterData()
            Try

                '更新
                Dim result = _customerService.UpdateCustomer(DetailData)

                If result.IsSuccess Then
                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "更新しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New CustomerClient()
                    IsCreateMaster = True
                Else
                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "更新失敗しました")
                End If

            Catch ex As Exception
                Log.Error(ex.Message & ex.StackTrace)
            End Try
        End Sub

        ''' <summary>
        '''     保存可能判断
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function CanSave() As Boolean
            If ErrorCount = 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Sub ImportMasterData()
            Dim openFileSelector As New OpenFileDialog

            openFileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            If openFileSelector.ShowDialog() = True Then

                Dim filePath As String = openFileSelector.FileName

                'Load Excel file
                Dim excelImport As New ExcelHelperInterop(filePath)
                'Import
                excelImport.Import()

                Dim dsExcel As DataSet = excelImport.DsExcel

                'Convert Excel data to Entities
                Dim customerClients As List(Of CustomerClient) = Dataset2Entities(dsExcel)

                For Each customer In customerClients
                    'Add to database
                    _customerService.CreateCustomer(customer)
                Next
            End If
        End Sub

        Public Overrides Sub ExportMasterData()
            Dim openFileSelector As New OpenFileDialog

            openFileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            If openFileSelector.ShowDialog() = True Then
                Dim fileName As String = openFileSelector.FileName

                'Change Entity to Datatable
                Dim dtExport As New DsClient.CustomersDataTable 

                For Each customer In MasterData
                    Dim row = dtExport.NewCustomersRow()

                    row.Name  = customer.Name
                    row.WechatName = customer.WechatName
                    row.TaobaoName  = customer.TaobaoName
                    row.Address = customer.Address
                    row.PostCode  = customer.PostCode
                    row.Phone = customer.Phone

                    dtExport.Rows.Add(row)
                Next

                Dim excelHelper As New ExcelHelperInterop(fileName, "Customers", dtExport)

                excelHelper.Export()
            End If
        End Sub

        ''' <summary>
        ''' Convert to entities
        ''' </summary>
        ''' <param name="ds"></param>
        ''' <returns></returns>
        Private Function Dataset2Entities(ds As DataSet) As List(Of CustomerClient)
            Dim result As New List(Of CustomerClient)

            If ds.Tables.Count > 0 Then

                For Each row As DataRow In ds.Tables(0).Rows
                    Dim client As New CustomerClient

                    client.Name = row.Item("Name").ToString()
                    client.WechatName = row.Item("WechatName").ToString()
                    client.TaobaoName = row.Item("TaobaoName").ToString()
                    client.Address = row.Item("Address").ToString()
                    client.PostCode = row.Item("PostCode").ToString()
                    client.Phone = row.Item("Phone").ToString()

                    result.Add(client)
                Next

            End If

            Return result
        End Function
    End Class
End Namespace