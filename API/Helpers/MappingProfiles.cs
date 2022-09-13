using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(dto => dto.ProductCollection, o => o.MapFrom(s => s.productCollection!.Name))
            .ForMember(dto => dto.ProductType, o => o.MapFrom(s => s.productType!.Name))
            .ForMember(dto => dto.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

        CreateMap<Post, PostRequestDto>().ReverseMap();
        CreateMap<ProductDto, Product>();
    }
}