namespace OrderService.Api.Integrations.Courier.Activities;

public interface PaymentLog
{
    Guid CustomerId { get; }
    double Credit { get; }
    
}