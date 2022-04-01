using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Messages;
using CustomerService.Api.Exceptions;
using CustomerService.Api.Services;
using MassTransit;

namespace CustomerService.Api.Integrations.Consumers;

public class ChargeCustomerConsumer : IConsumer<ChargeCustomerMessage>
{
    private readonly ILogger<ChargeCustomerConsumer> _logger;
    private readonly ICustomerService _customerService;

    public ChargeCustomerConsumer(ILogger<ChargeCustomerConsumer> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    public async Task Consume(ConsumeContext<ChargeCustomerMessage> context)
    {
        _logger.LogInformation("ChargeCustomerConsumer -> Get customer info for id {CustomerId}", context.Message.CustomerId);
        var customerData = await _customerService.GetCustomerByIdAsync(context.Message.CustomerId, context.CancellationToken);
        if (customerData is null)
        {
            throw new NotFoundException("Charge Customer Not Found",
                $"Customer info not found for id: {context.Message.CustomerId}");
        }
        // check to ensure we have enough funds, if overdrawn, throw exception
        _logger.LogInformation("Checking charge {Charge} against customer balance {Balance}", 
            context.Message.Charge, customerData.Balance);
        if (context.Message.Charge > customerData.Balance)
        {
            throw new OverdrawnException("Charge Customer Overdraft", $"Customer does not have enough funds for order with charge: {context.Message.Charge}");
        }

        var newBalance = customerData.Balance - context.Message.Charge;
        await _customerService.UpdateBalanceAsync(context.Message.CustomerId, newBalance, context.CancellationToken);
        await context.RespondAsync<CustomerResponse>(new {Result = 1});
    }
}
