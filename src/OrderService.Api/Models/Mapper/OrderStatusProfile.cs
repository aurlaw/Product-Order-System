using AurSystem.Framework.Mapper;
using AurSystem.Framework.Models;
using AutoMapper;

namespace OrderService.Api.Models.Mapper;

public class OrderStatusProfile : Profile
{
    public OrderStatusProfile()
    {
        CreateMap<string, OrderStatus>()
            .ConvertUsing<OrderStatusConverter>();
    }
}
