Imports MahApps.Metro.Controls

Class MainWindow
    ''' <summary>
    '''     コンストラクター
    ''' </summary>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    ''' <summary>
    '''     アイテム選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HamburgerMenuControl_OnItemClick(sender As Object, e As ItemClickEventArgs)
        Me.HamburgerMenuControl.Content = e.ClickedItem
        Me.HamburgerMenuControl.IsPaneOpen = False
    End Sub

    ''' <summary>
    '''     マスタ画面を開く
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnMaster_Click(sender As Object, e As RoutedEventArgs) Handles BtnMaster.Click
        Dim master As New MasterWindow
        master.Show()
    End Sub

End Class
