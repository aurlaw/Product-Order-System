using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api.Integrations.Courier.Activities;

public class PaymentActivity : IActivity<PaymentArgument, PaymentLog>
{
    private readonly ILogger<PaymentActivity> _logger;
    private readonly IEndpointNameFormatter _formatter;

    public PaymentActivity(ILogger<PaymentActivity> logger, IEndpointNameFormatter formatter)
    {
        _logger = logger;
        _formatter = formatter;
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<PaymentArgument> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_formatter.Message<ChargeCustomerMessage>()}");
        throw new NotImplementedException();
    }

    public async Task<CompensationResult> Compensate(CompensateContext<PaymentLog> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_formatter.Message<CreditCustomerMessage>()}");
        throw new NotImplementedException();
    }
}

/*
 
 *            logger.LogInformation($"Payment Courier called for order {context.Arguments.OrderId}");
            var uri = QueueNames.GetMessageUri(nameof(WithdrawCustomerCreditMessage));
            var sendEndpoint = await context.GetSendEndpoint(uri);
            await sendEndpoint.Send<WithdrawCustomerCreditMessage>(new
            {                
                Credit = context.Arguments.Credit,
                CustomerId = context.Arguments.CustomerId,
                OrderId = context.Arguments.OrderId
            });
            return context.Completed(new { CustomerId  = context.Arguments.CustomerId, Credit = context.Arguments.Credit });

 *
 * 
 */