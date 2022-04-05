using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api.Integrations.Courier.Activities;

public class PaymentActivity : IActivity<PaymentArgument, PaymentLog>
{
    private readonly ILogger<PaymentActivity> _logger;
    private readonly MessageMapper _messageMapper;

    public PaymentActivity(ILogger<PaymentActivity> logger, MessageMapper messageMapper)
    {
        _logger = logger;
        _messageMapper = messageMapper;
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<PaymentArgument> context)
    {
        //queue or exchange
        // var queueName = _formatter.Message<ChargeCustomerMessage>();
        var address = new Uri($"exchange:{_messageMapper.GetMessageName<ChargeCustomerMessage>()}");
        _logger.LogInformation("Execute Payment: {Charge} {address}", context.Arguments.Charge, address);
        var sendEndpoint = await context.GetSendEndpoint(address);
        await sendEndpoint.Send<ChargeCustomerMessage>(new
        {
            context.Arguments.CustomerId,
            context.Arguments.Charge
        });
        var paymentLog = new
        {
            context.Arguments.CustomerId,
            Credit = context.Arguments.Charge
        };
        return context.Completed<PaymentLog>(paymentLog);

    }

    public async Task<CompensationResult> Compensate(CompensateContext<PaymentLog> context)
    {
        //queue or exchange
        var address = new Uri($"exchange:{_messageMapper.GetMessageName<CreditCustomerMessage>()}");
        _logger.LogInformation("Compensate Payment: {Credit} - {address}", context.Log.Credit, address);
        var sendEndpoint = await context.GetSendEndpoint(address);
        await sendEndpoint.Send<CreditCustomerMessage>(new
        {
            context.Log.CustomerId,
            context.Log.Credit
        });
        return context.Compensated();

    }
}