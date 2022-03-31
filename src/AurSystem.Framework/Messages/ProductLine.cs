namespace AurSystem.Framework.Messages;

public interface ProductLine
{
    Guid ProductId { get; }
    int Quantity { get; }
}