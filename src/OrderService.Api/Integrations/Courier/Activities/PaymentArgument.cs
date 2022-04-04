namespace OrderService.Api.Integrations.Courier.Activities;

public interface PaymentArgument
{
    Guid CustomerId { get; }
    double Charge { get; }
}