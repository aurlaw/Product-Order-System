namespace AurSystem.Framework.Messages;

public interface OrderFaulted : FutureFaulted
{
    Guid OrderId { get; }
    string Description { get; }
    
}