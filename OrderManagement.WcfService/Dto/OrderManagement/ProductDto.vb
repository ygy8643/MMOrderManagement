Namespace Dto.OrderManagement

    Public Class ProductDto

        Public Property ProductId As Integer
        Public Property BrandId As Integer
        Public Property ProductName As String
        Public Property ProductNameJp As String
        Public Property SpeciesId As Integer

        'Navigation Properties
        Public Property BrandDto As BrandDto 
        Public Property SpeciesDto As SpeciesDto 

    End Class
End Namespace
