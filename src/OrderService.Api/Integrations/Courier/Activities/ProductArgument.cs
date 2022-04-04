using AurSystem.Framework.Messages;

namespace OrderService.Api.Integrations.Courier.Activities;

public interface ProductArgument
{
    Guid OrderId { get; }
    IList<ProductLine> Lines { get; }

}