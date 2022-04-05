using MassTransit;
using OrderService.Api.Services;

namespace OrderService.Api.Integrations.Courier.Activities;

public class CompleteOrderActivity : IExecuteActivity<OrderArgument>
{
    private readonly ILogger<CompleteOrderActivity> _logger;
    private readonly IOrderService _orderService;

    public CompleteOrderActivity(ILogger<CompleteOrderActivity> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public Task<ExecutionResult> Execute(ExecuteContext<OrderArgument> context)
    {
        _logger.LogInformation("Execute Complete Order: {OrderId}", context.Arguments.OrderId);
        return Task.FromResult(context.Completed());
    }
}