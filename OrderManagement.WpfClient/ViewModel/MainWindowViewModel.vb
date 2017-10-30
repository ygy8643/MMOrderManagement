Imports GalaSoft.MvvmLight
Imports OrderManagement.WpfClient.Service
Imports OrderManagement.WpfClient.ViewModel.Base
Imports OrderManagement.WpfClient.ViewModel.Order
Imports OrderManagement.WpfClient.WcfService

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
            Set
                [Set]("CurrentUserControlViewModel", _currentUserControlViewModel, Value)
            End Set
        End Property

        ''' <summary>
        ''' </summary>
        Public Sub New()

            Dim orderService = New OrderManagementServiceClient

            CurrentUserControlViewModel = New ListOrderViewModel(
                New FrameNavigationService(),
                New OrderServiceAgent(),
                New CustomerServiceAgent()
                )
        End Sub
    End Class
End Namespace
