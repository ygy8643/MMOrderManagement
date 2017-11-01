Imports MahApps.Metro.Controls.Dialogs
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.WpfClient.Service

Namespace ViewModel.Master
    Public Class SpeciesViewModel
        Inherits BaseMasterViewModel(Of SpeciesClient)

#Region "フィールド"

        ''' <summary>
        '''     サービス
        ''' </summary>
        Private ReadOnly _speciesService As ISpeciesServiceAgent

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
        Private _selectedData As SpeciesClient

        Public Property SelectedData As SpeciesClient
            Get
                Return _selectedData
            End Get
            Set
                [Set]("SelectedData", _selectedData, Value)

                If Value Is Nothing OrElse
                   String.IsNullOrEmpty(Value.SpeciesId) OrElse Value.SpeciesId = 0 Then
                    DetailData = New SpeciesClient
                Else

                    'リストからデータを選択する時には修正モードに切り替え、詳細をセット
                    IsCreateMaster = False
                    DetailData = New SpeciesClient With {
                        .SpeciesId = Value.SpeciesId,
                        .SpeciesName = Value.SpeciesName}
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
        Public Sub New(dataService As ISpeciesServiceAgent, dialogInstance As IDialogCoordinator)

            MyBase.New()

            SelectedData = New SpeciesClient()
            SearchCondition = New SpeciesClient()

            'サービスの初期化
            _speciesService = dataService
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
                    _speciesService.SpeciesExists(DetailData.SpeciesId) Then

                    _dialogCoordinator.ShowMessageAsync(Me, "エラー", "データが重複しました")
                Else

                    '新規追加
                    Dim result = _speciesService.CreateSpecies(DetailData)

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
            DetailData = New SpeciesClient()
            SelectedData = New SpeciesClient()
        End Sub

        ''' <summary>
        '''     削除
        ''' </summary>
        Public Overrides Sub DeleteMasterData()
            Try

                '削除
                Dim result = _SpeciesService.DeleteSpecies(DetailData.SpeciesId)

                If result.IsSuccess Then

                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "削除しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New SpeciesClient()
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
                MasterData = _SpeciesService.GetSpeciesByCondition(SearchCondition)

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
                Dim result = _SpeciesService.UpdateSpecies(DetailData)

                If result.IsSuccess Then
                    _dialogCoordinator.ShowMessageAsync(Me, "メッセージ", "更新しました")

                    '結果リストをリセット
                    SearchMasterData()

                    SelectedData = New SpeciesClient()
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
    End Class
End Namespace