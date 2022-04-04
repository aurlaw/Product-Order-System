using AurSystem.Framework.Messages;
using AurSystem.Framework.Models.Domain;
using MassTransit;
using MassTransit.Courier;

namespace OrderService.Api.Integrations.Futures;

public class OrderFuture : Future<SubmitOrder, OrderCompleted, OrderFaulted>
{
    public OrderFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.OrderId));

        ExecuteRoutingSlip(x =>
            {
                x.OnRoutingSlipCompleted(r => r
                    .SetCompletedUsingInitializer(context =>
                    {
                        var order = context.GetVariable<Order>(nameof(OrderCompleted.Order));
                        return new
                        {
                            OrderId = order.Id,
                            Order = order,
                            Status = order.Status.ToString()
                        };
                    }));
                x.OnRoutingSlipFaulted(faulted => faulted
                    .SetFaultedUsingInitializer(context =>
                    {
                        var faults = context.Saga.Faults
                            .ToDictionary(k => k.Key, 
                                v => context.ToObject<Fault>(v.Value));
                        return new
                        {
                            Faulted = context.Saga.Faulted,
                            Exceptions = faults.SelectMany(x => x.Value.Exceptions).ToArray()
                        };
                    }));
            }
            );
    }   
}