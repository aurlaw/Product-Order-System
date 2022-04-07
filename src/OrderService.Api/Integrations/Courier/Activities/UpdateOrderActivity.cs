using MassTransit;
using OrderService.Api.Services;

namespace OrderService.Api.Integrations.Courier.Activities;

public class UpdateOrderActivity : IExecuteActivity<UpdateOrderArgument>
{
    private readonly ILogger<UpdateOrderActivity> _logger;
    private readonly IOrderService _orderService;

    public UpdateOrderActivity(ILogger<UpdateOrderActivity> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<UpdateOrderArgument> context)
    {
        _logger.LogInformation("Execute Complete Order: {OrderId}", context.Arguments.OrderId);

        await _orderService.UpdateOrderStatus(context.Arguments.OrderId, context.Arguments.Status,
            context.CancellationToken);

        var order = context.Arguments.Order;
        order.Status = context.Arguments.Status;
        return context.CompletedWithVariables(new {order});
    }
}
