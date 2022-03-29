using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;
using AutoMapper;

namespace OrderService.Api.Models.Mapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderEntity>()
            .ReverseMap();
        CreateMap<OrderItem, OrderItemEntity>()
            .ReverseMap();
        CreateMap<OrderDto, Order>()
            .ReverseMap();
        CreateMap<OrderItemDto, OrderItem>()
            .ReverseMap();

    }
}