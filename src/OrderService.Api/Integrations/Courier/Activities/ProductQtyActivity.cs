using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api.Integrations.Courier.Activities;

public class ProductQtyActivity : IActivity<ProductArgument, ProductLog>
{
    private readonly ILogger<ProductQtyActivity> _logger;
    private readonly IEndpointNameFormatter _formatter;

    public ProductQtyActivity(ILogger<ProductQtyActivity> logger, IEndpointNameFormatter formatter)
    {
        _logger = logger;
        _formatter = formatter;
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<ProductArgument> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_formatter.Message<TakeProductMessage>()}");
        
        throw new NotImplementedException();
    }

    public async Task<CompensationResult> Compensate(CompensateContext<ProductLog> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_formatter.Message<ReturnProductMessage>()}");
        throw new NotImplementedException();
    }
}