using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;

namespace AurSystem.Framework.Models;

public static class OrderFactory
{
    public static Order Create(CreateOrderDto orderDto)
    {
        var order = new Order
        {
            CustomerId = orderDto.CustomerId,
            OrderNumber = OrderNumGenerator.Generate("W-", id: 5),
            Status = OrderStatus.Pending
        };
        orderDto.LineItems.ForEach(item =>
        {
            var lineItem = new OrderItem
            {
                ProductId = item.ProductId,
                Qty = item.Qty,
                Total = item.Qty * item.Price,
            };
            order.LineItems.Add(lineItem);
        });
        order.Total = order.LineItems.Sum(line => line.Total);
        return order;
    }
}