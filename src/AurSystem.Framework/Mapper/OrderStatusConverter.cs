using AurSystem.Framework.Models;
using AutoMapper;

namespace AurSystem.Framework.Mapper;

public class OrderStatusConverter : ITypeConverter<string, OrderStatus>
{
    public OrderStatus Convert(string source, OrderStatus destination, ResolutionContext context)
    {
        return OrderStatus.FromName(source);
    }
}

