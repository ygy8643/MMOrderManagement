Imports System.ComponentModel
Imports PropertyChanged

Namespace Models.OrderManagement

    <ImplementPropertyChanged>
    Public Class ProductClient
        <DisplayName("产品编号")>
        Public Property ProductId As Integer

        <DisplayName("种类编号")>
        Public Property SpeciesId As Integer

        <DisplayName("品牌编号")>
        Public Property BrandId As Integer

        <DisplayName("产品中文名称")>
        Public Property ProductName As String

        <DisplayName("产品日文名称")>
        Public Property ProductNameJp As String
                
        'Navigation Properties
        Public Property BrandClient As BrandClient 
        Public Property SpeciesClient As SpeciesClient
    End Class
End Namespace