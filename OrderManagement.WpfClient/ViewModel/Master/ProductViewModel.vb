Imports System.Data
Imports MahApps.Metro.Controls.Dialogs
Imports Microsoft.Win32
Imports OrderManagement.Client.Entities
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common
Imports OrderManagement.Common.ExcelExport.Interop
Imports OrderManagement.WpfClient.Service
Imports OrderManagement.WpfClient.Service.Interfaces

Namespace ViewModel.Master
    Public Class ProductViewModel
        Inherits BaseMasterViewModel(Of ProductClient)

#Region "フィールド"

        ''' <summary>
        '''     サービス
        ''' </summary>
        Private ReadOnly _productService As IProductServiceAgent

        ''' <summary>
        '''     ダイアログ
        ''' </summary>
        Private ReadOnly _dialogCoordinator As IDialogCoordinator

        ''' <summary>
        '''     リストサービス
        ''' </summary>
        Private ReadOnly _listService As IListServiceAgent

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
        Private _selectedData As ProductClient

        Public Property SelectedData As ProductClient
            Get
                Return _selectedData
            End Get
            Set
                [Set]("SelectedData", _selectedData, Value)

                If Value Is Nothing OrElse
                   String.IsNullOrEmpty(Value.ProductId) OrElse Value.ProductId = 0 Then
                    DetailData = New ProductClient
                Else

                    'リストからデータを選択する時には修正モードに切り替え、詳細をセット
                    IsCreateMaster = False
                    DetailData = New ProductClient With {
                        .ProductId = Value.ProductId,
                        .SpeciesId = Value.SpeciesId,
                        .BrandId = Value.BrandId,
                        .ProductName = Value.ProductName,
                        .ProductNameJp = Value.ProductNameJp}
                End If
            End Set
        End Property

#End Region

#Region "リスト"

        ''' <summary>
        '''     种类列表
        ''' </summary>
        Public Property SpeciesList As List(Of ValueNamePair)

        ''' <summary>
        '''     品牌列表
        ''' </summary>
        ''' <returns></returns>
        Public Property BrandList As List(Of ValueNamePair)

#End Region

#Region "コンストラクター"

        ''' <summary>
        '''     初期化
        ''' </summary>
        ''' <param name="dataService"></param>
        ''' <remarks></remarks>
        Public Sub New(dataService As IProductServiceAgent, dialogInstance As IDialogCoordinator,
                       listService As IListServiceAgent)

            MyBase.New()

            SelectedData = New ProductClient()
            SearchCondition = New ProductClient()

            'サービスの初期化
            _productService = dataService
            _dialogCoordinator = dialogInstance
            _listService = listService

            'リストの初期化
            SpeciesList = listService.GetSpeciesList()
            BrandList = listService.GetBrandList()
        End Sub

#End Region

        ''' <summary>
        '''     追加
        ''' </summary>
        Public Overrides Sub AddMasterData()

            Try
                '存在チェック
                If _
                    _productService.ProductExists(DetailData.ProductId) Then

                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "データが重複しました")
                Else

                    '新規追加
                    Dim result = _productService.CreateProduct(DetailData)

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
            DetailData = New ProductClient()
            SelectedData = New ProductClient()
        End Sub

        ''' <summary>
        '''     削除
        ''' </summary>
        Public Overrides Sub DeleteMasterData()
            Try

                '削除
                Dim result = _productService.DeleteProduct(DetailData.ProductId)

                If result.IsSuccess Then

                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "削除しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New ProductClient()
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
                MasterData = _productService.GetProductByCondition(SearchCondition)

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
                Dim result = _productService.UpdateProduct(DetailData)

                If result.IsSuccess Then
                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "更新しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New ProductClient()
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
                Dim productClients As List(Of ProductClient) = Dataset2Entities(dsExcel)

                For Each product In productClients
                    'Add to database
                    _productService.CreateProduct(product)
                Next
            End If
        End Sub

        Public Overrides Sub ExportMasterData()
            Dim openFileSelector As New OpenFileDialog

            openFileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            If openFileSelector.ShowDialog() = True Then
                Dim fileName As String = openFileSelector.FileName

                'Change Entity to Datatable
                Dim dtExport As New DsClient.ProductsDataTable

                For Each product In MasterData
                    Dim row = dtExport.NewProductsRow()

                    row.SpeciesId = product.SpeciesId
                    row.BrandId = product.BrandId
                    row.ProductName = product.ProductName
                    row.ProductNameJp = product.ProductNameJp

                    dtExport.Rows.Add(row)
                Next

                Dim excelHelper As New ExcelHelperInterop(fileName, "Products", dtExport)

                excelHelper.Export()
            End If
        End Sub

        ''' <summary>
        '''     Convert to entities
        ''' </summary>
        ''' <param name="ds"></param>
        ''' <returns></returns>
        Private Function Dataset2Entities(ds As DataSet) As List(Of ProductClient)
            Dim result As New List(Of ProductClient)

            If ds.Tables.Count > 0 Then

                For Each row As DataRow In ds.Tables(0).Rows
                    Dim client As New ProductClient

                    client.SpeciesId = row.Item("SpeciesId").ToString()
                    client.BrandId = row.Item("BrandId").ToString()
                    client.ProductName = row.Item("ProductName").ToString()
                    client.ProductNameJp = row.Item("ProductNameJp").ToString()

                    result.Add(client)
                Next

            End If

            Return result
        End Function
    End Class
End Namespace