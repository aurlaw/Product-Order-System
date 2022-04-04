using AurSystem.Framework.Models;

namespace OrderService.Api.Integrations.Courier.Activities;

public interface OrderArgument
{
    public Guid OrderId { get; }
    public OrderStatus Status { get; }
}