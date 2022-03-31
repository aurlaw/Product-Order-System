namespace AurSystem.Framework.Messages;

public interface CreditCustomerMessage
{
    Guid CustomerId { get; }
    double Credit { get; }
}