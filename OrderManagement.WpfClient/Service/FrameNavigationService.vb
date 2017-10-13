Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports GalaSoft.MvvmLight.Views

Namespace Service
    Public Class FrameNavigationService
        Implements IFrameNavigationService, INotifyPropertyChanged

#Region "Fields"

        Private ReadOnly _pagesByKey As Dictionary(Of String, Uri)
        Private ReadOnly _historic As List(Of String)
        Public Property Parameter As Object Implements IFrameNavigationService.Parameter
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

#Region "Properties"

        Private _currentPageKey As String

        Public Property CurrentPageKey As String Implements INavigationService.CurrentPageKey
            Get
                Return _currentPageKey
            End Get
            Set
                If _currentPageKey = Value Then
                    Return
                End If

                _currentPageKey = Value
                OnPropertyChanged("CurrentPageKey")
            End Set
        End Property

#End Region

#Region "Constructor"

        Public Sub New()
            _pagesByKey = New Dictionary(Of String, Uri)()
            _historic = New List(Of String)()
        End Sub

#End Region

#Region "Methods"

        Public Sub GoBack() Implements INavigationService.GoBack
            If _historic.Count > 1 Then
                _historic.RemoveAt(_historic.Count - 1)
                NavigateTo(_historic.Last(), Nothing)
            End If
        End Sub

        Public Sub NavigateTo(pageKey As String) Implements INavigationService.NavigateTo
            NavigateTo(pageKey, Nothing)
        End Sub

        Public Sub NavigateTo(pageKey As String, para As Object) Implements INavigationService.NavigateTo
            SyncLock _pagesByKey
                If Not _pagesByKey.ContainsKey(pageKey) Then
                    Throw New ArgumentException(String.Format("No such page: {0} ", pageKey), "pageKey")
                End If

                Dim frame = TryCast(GetDescendantFromName(Application.Current.MainWindow, "MainFrame"), Frame)

                If frame IsNot Nothing Then
                    frame.Source = _pagesByKey(pageKey)
                End If
                Parameter = para
                _historic.Add(pageKey)
                CurrentPageKey = pageKey
            End SyncLock
        End Sub

        Private Shared Function GetDescendantFromName(parent As DependencyObject, name As String) As FrameworkElement
            Dim count = VisualTreeHelper.GetChildrenCount(parent)

            If count < 1 Then
                Return Nothing
            End If

            For i = 0 To count - 1
                Dim frameworkElement = TryCast(VisualTreeHelper.GetChild(parent, i), FrameworkElement)
                If frameworkElement IsNot Nothing Then
                    If frameworkElement.Name = name Then
                        Return frameworkElement
                    End If

                    frameworkElement = GetDescendantFromName(frameworkElement, name)
                    If frameworkElement IsNot Nothing Then
                        Return frameworkElement
                    End If
                End If
            Next
            Return Nothing
        End Function

        Public Sub Configure(key As String, pageType As Uri)
            SyncLock _pagesByKey
                If _pagesByKey.ContainsKey(key) Then
                    _pagesByKey(key) = pageType
                Else
                    _pagesByKey.Add(key, pageType)
                End If
            End SyncLock
        End Sub

        Protected Overridable Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

#End Region

    End Class
End Namespace