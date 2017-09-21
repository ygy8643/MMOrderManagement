Imports System.ComponentModel
Imports PropertyChanged

Namespace Models

    <ImplementPropertyChanged>
    Public Class BrandClient
        <DisplayName("品牌编号")>
        Public Property BrandId As Integer

        <DisplayName("品牌名称")>
        Public Property BrandName As String

        <DisplayName("品牌日文名称")>
        Public Property BrandNameJp As String
    End Class
End Namespace