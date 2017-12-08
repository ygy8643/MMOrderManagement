Imports System.Reflection
Imports GalaSoft.MvvmLight
Imports GalaSoft.MvvmLight.CommandWpf
Imports log4net

Namespace ViewModel.Master
    Public MustInherit Class BaseMasterViewModel(Of TMasterType As Class)
        Inherits ViewModelBase
        Implements IBaseMasterViewModel(Of TMasterType)

#Region "フィールド"

        'Log4Net
        Public Property Log As ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod.DeclaringType)

#End Region

#Region "プロパティ"

        ''' <summary>
        '''     新規画面フラグ
        ''' </summary>
        ''' <remarks></remarks>
        Private _isCreateMaster As Boolean

        Public Property IsCreateMaster As Boolean
            Get
                Return _isCreateMaster
            End Get
            Set
                [Set]("IsCreateMaster", _isCreateMaster, Value)
            End Set
        End Property

        ''' <summary>
        '''     検索条件
        ''' </summary>
        ''' <remarks></remarks>
        Private _searchCondition As TMasterType

        Public Property SearchCondition As TMasterType
            Get
                Return _searchCondition
            End Get
            Set
                [Set]("SearchConditionsCondition", _searchCondition, Value)
            End Set
        End Property

        ''' <summary>
        '''     詳細データ
        ''' </summary>
        ''' <remarks></remarks>
        Private _detailData As TMasterType

        Public Property DetailData As TMasterType
            Get
                Return _detailData
            End Get
            Set
                [Set]("DetailData", _detailData, Value)
            End Set
        End Property

        ''' <summary>
        '''     マスタ全データ
        ''' </summary>
        ''' <remarks></remarks>
        Private _masterData As IEnumerable(Of TMasterType)

        Public Property MasterData As IEnumerable(Of TMasterType)
            Get
                Return _masterData
            End Get
            Set
                [Set]("MasterData", _masterData, Value)
            End Set
        End Property

        ''' <summary>
        '''     処理結果メッセージ表示フラグ
        ''' </summary>
        ''' <remarks></remarks>
        Private _isShowResultFlag As Boolean

        Public Property IsShowResultFlag As Boolean
            Get
                Return _isShowResultFlag
            End Get
            Set
                [Set]("IsShowResultFlag", _isShowResultFlag, Value)
            End Set
        End Property

        ''' <summary>
        '''     処理結果メッセージ
        ''' </summary>
        ''' <remarks></remarks>
        Private _resultMessage As String

        Public Property ResultMessage As String
            Get
                Return _resultMessage
            End Get
            Set
                [Set]("ResultMessage", _resultMessage, Value)
            End Set
        End Property

#End Region

#Region "コマンド"

        ''' <summary>
        '''     ロード
        ''' </summary>
        Public Property LoadDataCommand As RelayCommand

        ''' <summary>
        '''     新規
        ''' </summary>
        Public Property AddDataCommand As RelayCommand

        ''' <summary>
        '''     更新
        ''' </summary>
        Public Property UpdataDataCommand As RelayCommand

        ''' <summary>
        '''     削除
        ''' </summary>
        Public Property DeleteDataCommand As RelayCommand

        ''' <summary>
        '''     検索
        ''' </summary>
        Public Property SearchDataCommand As RelayCommand

        ''' <summary>
        ''' 导入
        ''' </summary>
        ''' <returns></returns>
        Public Property ImportDataCommand As RelayCommand

        ''' <summary>
        ''' 导出
        ''' </summary>
        ''' <returns></returns>
        Public Property ExportDataCommand As RelayCommand

        ''' <summary>
        '''     クリア
        ''' </summary>
        Public Property ClearDataCommand As RelayCommand

        ''' <summary>
        '''     処理結果を隠す
        ''' </summary>
        Public Property HideSuccessMessageCommand As RelayCommand

#End Region

#Region "コンストラクター"

        ''' <summary>
        '''     初期化
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            'プロパティ初期化
            IsCreateMaster = True
            IsShowResultFlag = False
            ResultMessage = String.Empty

            With "コマンド初期化"

                'ロード
                LoadDataCommand = New RelayCommand(AddressOf LoadMasterData)

                '追加
                AddDataCommand = New RelayCommand(AddressOf AddMasterData, AddressOf CanSave)

                '更新
                UpdataDataCommand = New RelayCommand(AddressOf UpdateMasterData, AddressOf CanSave)

                '削除
                DeleteDataCommand = New RelayCommand(AddressOf DeleteMasterData)

                '検索
                SearchDataCommand = New RelayCommand(AddressOf SearchMasterData)

                '导入
                ImportDataCommand = New RelayCommand(AddressOf ImportMasterData)

                '导出
                ExportDataCommand = New RelayCommand(AddressOf ExportMasterData)

                'クリア
                ClearDataCommand = New RelayCommand(AddressOf ClearDetail)

                '成功メッセージを隠す
                HideSuccessMessageCommand = New RelayCommand(AddressOf HideSuccessMessage)

            End With
        End Sub

#End Region

#Region "メソッド"

        ''' <summary>
        '''     追加
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub AddMasterData() Implements IBaseMasterViewModel(Of TMasterType).AddMasterData

        ''' <summary>
        '''     クリア
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub ClearDetail() Implements IBaseMasterViewModel(Of TMasterType).ClearDetail

        ''' <summary>
        '''     削除
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub DeleteMasterData() Implements IBaseMasterViewModel(Of TMasterType).DeleteMasterData

        ''' <summary>
        '''     ロード
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub LoadMasterData() Implements IBaseMasterViewModel(Of TMasterType).LoadMasterData

        ''' <summary>
        '''     検索
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub SearchMasterData() Implements IBaseMasterViewModel(Of TMasterType).SearchMasterData

        ''' <summary>
        ''' インポート
        ''' </summary>
        Public MustOverride Sub ImportMasterData() Implements IBaseMasterViewModel(Of TMasterType).ImportMasterData

        ''' <summary>
        ''' エクスポート
        ''' </summary>
        Public MustOverride Sub ExportMasterData() Implements IBaseMasterViewModel(Of TMasterType).ExportMasterData

        ''' <summary>
        '''     更新
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub UpdateMasterData() Implements IBaseMasterViewModel(Of TMasterType).UpdateMasterData

        ''' <summary>
        '''     成功メッセージを隠す
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub HideSuccessMessage() Implements IBaseMasterViewModel(Of TMasterType).HideSuccessMessage

        ''' <summary>
        '''     保存可能判断
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function CanSave() As Boolean Implements IBaseMasterViewModel(Of TMasterType).CanSave

#End Region

#Region "IDisposable Support"

        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class
End Namespace