namespace AurSystem.Framework.Messages;

public interface ChargeCustomerMessage
{
    Guid CustomerId { get; }
    double Charge { get; }
}