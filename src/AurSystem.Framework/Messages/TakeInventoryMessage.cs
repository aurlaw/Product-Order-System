namespace AurSystem.Framework.Messages;

public interface TakeInventoryMessage
{
    IList<ProductLine> Lines { get; }
}