namespace AurSystem.Framework.Messages;

public interface TakeProductMessage
{
    IList<ProductLine> Lines { get; }
}