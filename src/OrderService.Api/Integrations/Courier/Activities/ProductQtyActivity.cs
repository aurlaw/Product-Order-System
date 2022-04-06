using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api.Integrations.Courier.Activities;

public class ProductQtyActivity : IActivity<ProductArgument, ProductLog>
{
    private readonly ILogger<ProductQtyActivity> _logger;
    private readonly MessageMapper _messageMapper;

    public ProductQtyActivity(ILogger<ProductQtyActivity> logger, MessageMapper messageMapper)
    {
        _logger = logger;
        _messageMapper = messageMapper;
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<ProductArgument> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_messageMapper.GetMessageName<TakeProductMessage>()}");
        _logger.LogInformation("Execute Product: {Count} {address}", context.Arguments.Lines?.Count, address);

        var sendEndpoint = await context.GetSendEndpoint(address);
        await sendEndpoint.Send<TakeProductMessage>(new { context.Arguments.Lines});

        var productLog = new
        {
            context.Arguments.OrderId,
            context.Arguments.Lines
        };
        return context.Completed<ProductLog>(productLog);

    }

    public async Task<CompensationResult> Compensate(CompensateContext<ProductLog> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_messageMapper.GetMessageName<ReturnProductMessage>()}");
        _logger.LogInformation("Compensate Product: {Count} - {address}", context.Log.Lines?.Count, address);
        
        var sendEndpoint = await context.GetSendEndpoint(address);
        await sendEndpoint.Send<ReturnProductMessage>(new { context.Log.Lines});

        return context.Compensated();
    }
}
