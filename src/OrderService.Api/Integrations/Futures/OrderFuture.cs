using AurSystem.Framework;
using AurSystem.Framework.Messages;
using AurSystem.Framework.Models.Domain;
using MassTransit;
using MassTransit.Courier;

namespace OrderService.Api.Integrations.Futures;

public class OrderFuture : Future<SubmitOrder, OrderCompleted, OrderFaulted>
{
    private readonly ILogger<OrderFuture> _logger;

    public OrderFuture(ILogger<OrderFuture> logger)
    {
        _logger = logger;
        ConfigureCommand(x => x.CorrelateById(context => context.Message.OrderId));

        ExecuteRoutingSlip(x =>
            {
                x.OnRoutingSlipCompleted(r => r
                    .SetCompletedUsingInitializer(context =>
                    {
                        var order = context.GetVariable<Order>(nameof(OrderCompleted.Order));
                        return new
                        {
                            OrderId = order?.Id,
                            Order = order,
                            Status = order?.Status.ToString()
                        };
                    }));
                x.OnRoutingSlipFaulted(faulted => faulted
                    .SetFaultedUsingInitializer(context =>
                    {
                        var faults = context.Saga.Faults
                            .ToDictionary(k => k.Key, 
                                v => context.ToObject<Fault>(v.Value));
                        var err = faults.Any() ? 
                            faults.SelectMany(x => x.Value.Exceptions).ToArray() : 
                            context.Message.ActivityExceptions.Select(e => e.ExceptionInfo).ToArray();
                        _logger.LogInformation("Setting fault: {Length}", err.Length);
                        return new
                        {
                            context.Saga.Created,
                            context.Saga.Faulted,
                            Exceptions = err,
                            Description = err.GetExceptionMessages()
                        };
                    }));
            }
            );
    }   
}