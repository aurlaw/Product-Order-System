using AurSystem.Framework.Messages;

namespace OrderService.Api.Integrations.Courier.Activities;

public interface ProductLog
{
    Guid OrderId { get; }
    IList<ProductLine> Lines { get; }

}