using AurSystem.Framework.Messages;
using CustomerService.Api.Services;
using MassTransit;

namespace CustomerService.Api.Integrations.Consumers;

public class CreditCustomerConsumer : IConsumer<CreditCustomerMessage>
{
    private readonly ILogger<CreditCustomerConsumer> _logger;
    private readonly ICustomerService _customerService;

    public CreditCustomerConsumer(ILogger<CreditCustomerConsumer> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    public async Task Consume(ConsumeContext<CreditCustomerMessage> context)
    {
        throw new NotImplementedException();
    }
}