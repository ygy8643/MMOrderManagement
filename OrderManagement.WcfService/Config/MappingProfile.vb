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
            CreateMap(GetType(Product), GetType(ProductDto)) _
                .ForMember("BrandDto",Sub(opt) opt.MapFrom("Brand")) _
                .ForMember("SpeciesDto",Sub(opt) opt.MapFrom("Species"))

            CreateMap(GetType(ProductDto), GetType(Product)) _
                .ForMember("Brand",Sub(opt) opt.MapFrom("BrandDto")) _
                .ForMember("Species",Sub(opt) opt.MapFrom("SpeciesDto"))

            'Species
            CreateMap(GetType(Species), GetType(SpeciesDto))
            CreateMap(GetType(SpeciesDto), GetType(Species))
            
            'Brand
            CreateMap(GetType(Brand), GetType(BrandDto))
            CreateMap(GetType(BrandDto), GetType(Brand))

            'Inventory
            CreateMap(GetType(Inventory), GetType(InventoryDto))
            CreateMap(GetType(InventoryDto), GetType(Inventory))

        End Sub

    End Class

End Namespace

