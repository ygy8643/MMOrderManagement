Imports AutoMapper
Imports OrderManagement.Entities
Imports OrderManagement.WcfService.Dto

Namespace Config

    Public Class MappingProfile
        Inherits Profile

        Protected Overrides Sub Configure()

            'Order
            CreateMap(GetType(Order), GetType(OrderDto)) _
            .ForMember("OrderDetailDtoes", Sub(opt) opt.MapFrom("OrderDetails")) _
            .ForMember("CustomerDto", Sub(opt) opt.MapFrom("Customer"))

            CreateMap(GetType(OrderDto), GetType(Order)) _
            .ForMember("OrderDetails", Sub(opt) opt.MapFrom("OrderDetailDtoes")) _
            .ForMember("Customer", Sub(opt) opt.MapFrom("CustomerDto"))

            'OrderDetail
            CreateMap(GetType(OrderDetail), GetType(OrderDetailDto)) _
            .ForMember("ProductDto", Sub(opt) opt.MapFrom("Product"))

            CreateMap(GetType(OrderDetailDto), GetType(OrderDetail)) _
            .ForMember("Product", Sub(opt) opt.MapFrom("ProductDto"))

            'Customer
            CreateMap(GetType(Customer), GetType(CustomerDto))
            CreateMap(GetType(CustomerDto), GetType(Customer))

            'Product
            CreateMap(GetType(Product), GetType(ProductDto))
            CreateMap(GetType(ProductDto), GetType(Product))

            'Inventory
            CreateMap(GetType(Inventory), GetType(InventoryDto))
            CreateMap(GetType(InventoryDto), GetType(Inventory))

        End Sub

    End Class

End Namespace

