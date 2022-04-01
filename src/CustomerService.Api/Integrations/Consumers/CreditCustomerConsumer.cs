using AurSystem.Framework.Exceptions;
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
        _logger.LogInformation("CreditCustomerConsumer -> Get customer info for id {CustomerId}", context.Message.CustomerId);
        var customerData = await _customerService.GetCustomerByIdAsync(context.Message.CustomerId, context.CancellationToken);
        if (customerData is null)
        {
            throw new NotFoundException("Credit Customer Not Found",
                $"Customer info not found for id: {context.Message.CustomerId}");
        }

        _logger.LogInformation("Credit customer with credit: {Credit} ", context.Message.Credit);
        var newBalance = customerData.Balance + context.Message.Credit;
        await _customerService.UpdateBalanceAsync(context.Message.CustomerId, newBalance, context.CancellationToken);
        await context.RespondAsync<CustomerResponse>(new {Result = 1});

    }
}