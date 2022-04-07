using AurSystem.Framework.Models;
using AurSystem.Framework.Models.Domain;

namespace OrderService.Api.Integrations.Courier.Activities;

public interface UpdateOrderArgument
{
    public Guid OrderId { get; }
    public Order Order { get; }
    public OrderStatus Status { get; }
    
}