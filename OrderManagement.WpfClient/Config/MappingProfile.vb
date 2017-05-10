Imports AutoMapper
Imports OrderManagement.Client.Entities
Imports OrderManagement.WpfClient.WcfService

Namespace Config

    Public Class MappingProfile
        Inherits Profile

        Protected Overrides Sub Configure()

            CreateMap(GetType(OrderClient), GetType(OrderDto)) _
            .ForMember("OrderDetailDtoes", Sub(opt) opt.MapFrom("OrderDetailClients")) _
            .ForMember("CustomerDto", Sub(opt) opt.MapFrom("CustomerClient"))

            CreateMap(GetType(OrderDto), GetType(OrderClient)) _
                .ForMember("OrderDetailClients", Sub(opt) opt.MapFrom("OrderDetailDtoes")) _
                .ForMember("CustomerClient", Sub(opt) opt.MapFrom("CustomerDto"))

            CreateMap(GetType(OrderDetailClient), GetType(OrderDetailDto))
            CreateMap(GetType(OrderDetailDto), GetType(OrderDetailClient))

            CreateMap(GetType(CustomerClient), GetType(CustomerDto))
            CreateMap(GetType(CustomerDto), GetType(CustomerClient))

        End Sub

    End Class

End Namespace

