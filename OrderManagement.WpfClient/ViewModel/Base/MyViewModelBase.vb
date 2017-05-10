Imports GalaSoft.MvvmLight

Public MustInherit Class MyViewModelBase
    Inherits ViewModelBase

    ''' <summary>
    ''' タイトル
    ''' </summary>
    Public MustOverride ReadOnly Property Title As String

End Class
