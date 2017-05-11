Imports GalaSoft.MvvmLight
Imports OrderManagement.WpfClient.Service

Namespace ViewModel
    Public Class MainWindowViewModel
        Inherits ViewModelBase

        ''' <summary>
        '''     表示する画面
        ''' </summary>
        ''' <remarks></remarks>
        Private _currentUserControlViewModel As MyViewModelBase

        Public Property CurrentUserControlViewModel As MyViewModelBase
            Get
                Return _currentUserControlViewModel
            End Get
            Set(value As MyViewModelBase)
                [Set]("CurrentUserControlViewModel", _currentUserControlViewModel, value)
            End Set
        End Property


        Public Sub New()
            CurrentUserControlViewModel = New OrderListViewModel(New OrderService)
        End Sub

    End Class
End Namespace
