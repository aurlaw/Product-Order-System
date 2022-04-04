using AurSystem.Framework.Messages;
using MassTransit;
using OrderService.Api.Services;

namespace OrderService.Api.Integrations.Courier.Activities;

public class OrderActivity : IActivity<OrderArgument, OrderLog>
{
    private readonly ILogger<OrderActivity> _logger;
    private readonly IOrderService _orderService;
    
    public OrderActivity(ILogger<OrderActivity> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<OrderArgument> context)
    {
        throw new NotImplementedException();
    }

    public async Task<CompensationResult> Compensate(CompensateContext<OrderLog> context)
    {
        throw new NotImplementedException();
    }
}
