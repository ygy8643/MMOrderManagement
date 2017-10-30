Namespace ViewModel.Master
    Public Interface IBaseMasterViewModel(Of TMasterType As Class)
        Inherits IDisposable

        ''' <summary>
        '''     マスタデータをロード
        ''' </summary>
        ''' <remarks></remarks>
        Sub LoadMasterData()

        ''' <summary>
        '''     マスタデータを追加
        ''' </summary>
        ''' <remarks></remarks>
        Sub AddMasterData()

        ''' <summary>
        '''     マスタデータを更新
        ''' </summary>
        ''' <remarks></remarks>
        Sub UpdateMasterData()

        ''' <summary>
        '''     マスタデータを削除
        ''' </summary>
        ''' <remarks></remarks>
        Sub DeleteMasterData()

        ''' <summary>
        '''     マスタデータを検索
        ''' </summary>
        ''' <remarks></remarks>
        Sub SearchMasterData()

        ''' <summary>
        '''     画面入力内容をクリア
        ''' </summary>
        ''' <remarks></remarks>
        Sub ClearDetail()

        ''' <summary>
        ''' 結果メッセージを隠す
        ''' </summary>
        ''' <remarks></remarks>
        Sub HideSuccessMessage()

        ''' <summary>
        ''' 保存可能判断
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CanSave() As Boolean

    End Interface
End Namespace