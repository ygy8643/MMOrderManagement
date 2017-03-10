Imports AutoMapper
Imports OrderManagement.Entities
Imports OrderManagement.Dtos

Public Class MappingProfile
    Inherits Profile

    Protected Overrides Sub Configure()
        CreateMap(Of Order, OrderDto)()
        CreateMap(Of OrderDto, Order)()
        CreateMap(Of OrderDetail, OrderDetailDto)()
        CreateMap(Of OrderDetailDto, OrderDetail)()
        CreateMap(Of Customer, CustomerDto)()
        CreateMap(Of CustomerDto, Customer)()
        CreateMap(Of Product, ProductDto)()
        CreateMap(Of ProductDto, Product)()
    End Sub
End Class
