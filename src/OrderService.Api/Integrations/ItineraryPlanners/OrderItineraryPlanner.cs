using AurSystem.Framework.Messages;
using AurSystem.Framework.Models;
using MassTransit;
using OrderService.Api.Integrations.Courier.Activities;

namespace OrderService.Api.Integrations.ItineraryPlanners;

public class OrderItineraryPlanner : IItineraryPlanner<SubmitOrder>
{
    private readonly Uri _orderAddress;
    private readonly Uri _paymentAddress;
    private readonly Uri _productAddress;
    private readonly Uri _completedOrderAddress;

    public OrderItineraryPlanner(IEndpointNameFormatter formatter)
    {
        _orderAddress = new Uri($"exchange:{formatter.ExecuteActivity<OrderActivity, OrderArgument>()}");
        _paymentAddress = new Uri($"exchange:{formatter.ExecuteActivity<PaymentActivity, PaymentArgument>()}");
        _productAddress = new Uri($"exchange:{formatter.ExecuteActivity<ProductQtyActivity, ProductArgument>()}");
        _completedOrderAddress = new Uri($"exchange:{formatter.ExecuteActivity<UpdateOrderActivity, UpdateOrderArgument>()}");
    }
    
    public  Task PlanItinerary(BehaviorContext<FutureState, SubmitOrder> value, IItineraryBuilder builder)
    {
        var order = value.Message;
        
        builder.AddVariable(nameof(SubmitOrder.OrderId), order.OrderId);
        
        // order activity
        builder.AddActivity(nameof(OrderActivity), _orderAddress, new
        {
            Status = OrderStatus.Processing
        });
        // payment activity
        builder.AddActivity(nameof(PaymentActivity), _paymentAddress, new
        {
        });
        // product activity
        builder.AddActivity(nameof(ProductQtyActivity), _productAddress, new
        {
        });
        // order completed activity
        builder.AddActivity(nameof(UpdateOrderActivity), _completedOrderAddress, new
        {
            Status = OrderStatus.Ready
        });

        return Task.CompletedTask;
    }
}
