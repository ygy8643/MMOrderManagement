Imports System.ComponentModel
Imports PropertyChanged

<ImplementPropertyChanged>
Public Class Species
    <DisplayName("种类编号")>
    Public Property SpeciesId As Integer
    <DisplayName("种类名称")>
    Public Property SpeciesName As String
End Class
