using AurSystem.Framework.Exceptions;
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
        _logger.LogInformation("Execute Order: {OrderId}", context.Arguments.OrderId);
        var order = await _orderService.GetOrderByIdAsync(context.Arguments.OrderId, context.CancellationToken);
        if (order is null)
        {
            throw new NotFoundException("Order Not Found", $"Order not found for ID: {context.Arguments.OrderId}");
        }
        var initialStatus = order.Status;
        // update status
        await _orderService.UpdateOrderStatus(order.Id, context.Arguments.Status, context.CancellationToken);

        var orderLog = new
        {
            Order = order,
            Status = initialStatus
        };
        var upOrder = order;
        upOrder.Status = context.Arguments.Status;
        var orderArgs = new
        {
            Order = upOrder,
            upOrder.CustomerId,
            Charge = upOrder.Total,
            Lines = upOrder.LineItems.Select(line => new
            {
                line.ProductId,
                Quantity = line.Qty
            }).ToList()
        };

        return context.CompletedWithVariables<OrderLog>(orderLog, orderArgs);
    }
    
    public async Task<CompensationResult> Compensate(CompensateContext<OrderLog> context)
    {
        _logger.LogInformation("Compensate Order: {Id}", context.Log.Order.Id);
        var orderId = context.Log.Order.Id;
        var initStatus = context.Log.Status;
        
        // update status
        await _orderService.UpdateOrderStatus(orderId, initStatus, context.CancellationToken);

        return context.Compensated();

    }
}
