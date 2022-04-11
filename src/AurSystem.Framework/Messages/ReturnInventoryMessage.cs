namespace AurSystem.Framework.Messages;

public interface ReturnInventoryMessage
{
    IList<ProductLine> Lines { get; }
}