Imports System.ComponentModel
Imports PropertyChanged

Namespace Models.OrderManagement

    <ImplementPropertyChanged>
    Public Class SpeciesClient
        <DisplayName("种类编号")>
        Public Property SpeciesId As Integer

        <DisplayName("种类名称")>
        Public Property SpeciesName As String
    End Class
End Namespace