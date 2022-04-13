namespace AurSystem.Framework.Messages;

public interface AddInventoryMessage
{
    IList<ProductLine> Lines { get; }
}