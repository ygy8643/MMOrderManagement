Imports System.ComponentModel
Imports PropertyChanged

Namespace Models

    <ImplementPropertyChanged>
    Public Class ProductClient
        <DisplayName("产品编号")>
        Public Property ProductId As Integer

        <DisplayName("产品中文名称")>
        Public Property ProductName As String

        <DisplayName("产品日文名称")>
        Public Property ProductNameJp As String
    End Class
End Namespace