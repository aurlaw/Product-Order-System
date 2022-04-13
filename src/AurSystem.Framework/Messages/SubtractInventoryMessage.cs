namespace AurSystem.Framework.Messages;

public interface SubtractInventoryMessage
{
    IList<ProductLine> Lines { get; }
}