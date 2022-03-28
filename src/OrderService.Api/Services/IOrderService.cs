using AurSystem.Framework.Models.Domain;

namespace OrderService.Api.Services;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken token = default);
}
