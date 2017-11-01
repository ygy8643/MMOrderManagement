Imports AutoMapper
Imports OrderManagement.Client.Entities
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Client.Entities.SearchConditions
Imports OrderManagement.WpfClient.WcfService

Namespace Config

    Public Class MappingProfile
        Inherits Profile

        Protected Overrides Sub Configure()

            'Order
            CreateMap(GetType(OrderClient), GetType(OrderDto)) _
                .ForMember("OrderDetailDtoes", Sub(opt) opt.MapFrom("OrderDetailClients")) _
                .ForMember("CustomerDto", Sub(opt) opt.MapFrom("CustomerClient"))

            CreateMap(GetType(OrderDto), GetType(OrderClient)) _
                .ForMember("OrderDetailClients", Sub(opt) opt.MapFrom("OrderDetailDtoes")) _
                .ForMember("CustomerClient", Sub(opt) opt.MapFrom("CustomerDto"))

            'OrderDetail
            CreateMap(GetType(OrderDetailClient), GetType(OrderDetailDto)) _
                .ForMember("ProductDto", Sub(opt) opt.MapFrom("ProductClient"))
            CreateMap(GetType(OrderDetailDto), GetType(OrderDetailClient)) _
                .ForMember("ProductClient", Sub(opt) opt.MapFrom("ProductDto"))

            'Customer
            CreateMap(GetType(CustomerClient), GetType(CustomerDto))
            CreateMap(GetType(CustomerDto), GetType(CustomerClient))

            'Product
            CreateMap(GetType(ProductClient), GetType(ProductDto))
            CreateMap(GetType(ProductDto), GetType(ProductClient))

            'Species
            CreateMap(GetType(SpeciesClient), GetType(SpeciesDto))
            CreateMap(GetType(SpeciesDto), GetType(SpeciesClient))

            'Search Conditions
            CreateMap(GetType(OrderSearchConditionsClient), GetType(OrderSearchConditionsDto))
            CreateMap(GetType(OrderSearchConditionsDto), GetType(OrderSearchConditionsClient))
        End Sub

    End Class

End Namespace

