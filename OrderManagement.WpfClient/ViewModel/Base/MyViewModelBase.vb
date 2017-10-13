Imports GalaSoft.MvvmLight

Namespace ViewModel.Base

    Public MustInherit Class MyViewModelBase
        Inherits ViewModelBase

        ''' <summary>
        ''' タイトル
        ''' </summary>
        Public MustOverride ReadOnly Property Title As String

    End Class
End Namespace