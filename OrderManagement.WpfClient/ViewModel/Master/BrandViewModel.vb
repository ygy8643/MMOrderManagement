Imports System.Data
Imports MahApps.Metro.Controls.Dialogs
Imports Microsoft.Win32
Imports OrderManagement.Client.Entities
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common.ExcelExport.Interop
Imports OrderManagement.WpfClient.Service

Namespace ViewModel.Master
    Public Class BrandViewModel
        Inherits BaseMasterViewModel(Of BrandClient)

#Region "フィールド"

        ''' <summary>
        '''     サービス
        ''' </summary>
        Private ReadOnly _brandService As IBrandServiceAgent

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
                Return "种类信息"
            End Get
        End Property

        ''' <summary>
        '''     選択したデータ
        ''' </summary>
        ''' <remarks></remarks>
        Private _selectedData As BrandClient

        Public Property SelectedData As BrandClient
            Get
                Return _selectedData
            End Get
            Set
                [Set]("SelectedData", _selectedData, Value)

                If Value Is Nothing OrElse
                   String.IsNullOrEmpty(Value.BrandId) OrElse Value.BrandId = 0 Then
                    DetailData = New BrandClient
                Else

                    'リストからデータを選択する時には修正モードに切り替え、詳細をセット
                    IsCreateMaster = False
                    DetailData = New BrandClient With {
                        .BrandId = Value.BrandId,
                        .BrandName = Value.BrandName,
                        .BrandNameJp = Value.BrandNameJp}
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
        Public Sub New(dataService As IBrandServiceAgent, dialogInstance As IDialogCoordinator)

            MyBase.New()

            SelectedData = New BrandClient()
            SearchCondition = New BrandClient()

            'サービスの初期化
            _brandService = dataService
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
                If _
                    _brandService.BrandExists(DetailData.BrandId) Then

                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "データが重複しました")
                Else

                    '新規追加
                    Dim result = _brandService.CreateBrand(DetailData)

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
            DetailData = New BrandClient()
            SelectedData = New BrandClient()
        End Sub

        ''' <summary>
        '''     削除
        ''' </summary>
        Public Overrides Sub DeleteMasterData()
            Try

                '削除
                Dim result = _brandService.DeleteBrand(DetailData.BrandId)

                If result.IsSuccess Then

                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "削除しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New BrandClient()
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
                MasterData = _brandService.GetBrandByCondition(SearchCondition)

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
                Dim result = _brandService.UpdateBrand(DetailData)

                If result.IsSuccess Then
                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "更新しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New BrandClient()
                    IsCreateMaster = True
                Else
                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "更新失敗しました")
                End If

            Catch ex As Exception
                Log.Error(ex.Message & ex.StackTrace)
            End Try
        End Sub

        ''' <summary>
        ''' インポート
        ''' </summary>
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
                Dim brandClients As List(Of BrandClient) = Dataset2Entities(dsExcel)

                For Each brand In brandClients
                    'Add to database
                    _brandService.CreateBrand(brand)
                Next
            End If
        End Sub

        ''' <summary>
        ''' エクスポート
        ''' </summary>
        Public Overrides Sub ExportMasterData()
            Dim openFileSelector As New OpenFileDialog

            openFileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            If openFileSelector.ShowDialog() = True Then
                Dim fileName As String = openFileSelector.FileName

                'Change Entity to Datatable
                Dim dtExport As New DsClient.BrandsDataTable

                For Each brand In MasterData
                    Dim row = dtExport.NewBrandsRow()

                    row.BrandName = brand.BrandName
                    row.BrandNameJp = brand.BrandNameJp

                    dtExport.Rows.Add(row)
                Next

                Dim excelHelper As New ExcelHelperInterop(fileName, "Brands", dtExport)

                excelHelper.Export()
            End If
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

        ''' <summary>
        ''' Convert to entities
        ''' </summary>
        ''' <param name="ds"></param>
        ''' <returns></returns>
        Private Function Dataset2Entities(ds As DataSet) As List(Of BrandClient)
            Dim result As New List(Of BrandClient)

            If ds.Tables.Count > 0 Then

                For Each row As DataRow In ds.Tables(0).Rows
                    Dim brand As New BrandClient

                    brand.BrandName = row.Item("BrandName").ToString()
                    brand.BrandNameJp = row.Item("BrandNameJp").ToString()

                    result.Add(brand)
                Next

            End If

            Return result
        End Function

    End Class
End Namespace