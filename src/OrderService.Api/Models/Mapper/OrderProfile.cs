using AurSystem.Framework.Models.Domain;
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
    }
}