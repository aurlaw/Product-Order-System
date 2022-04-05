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
    public Task<ExecutionResult> Execute(ExecuteContext<ProductArgument> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_messageMapper.GetMessageName<TakeProductMessage>()}");
        
        _logger.LogInformation("Execute Product: {Count} {address}", context.Arguments.Lines?.Count, address);
        return Task.FromResult(context.Completed());
    }

    public Task<CompensationResult> Compensate(CompensateContext<ProductLog> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_messageMapper.GetMessageName<ReturnProductMessage>()}");
        _logger.LogInformation("Compensate Product: {Elapsed} - {address}", context.Elapsed, address);
        return Task.FromResult(context.Compensated());
    }
}