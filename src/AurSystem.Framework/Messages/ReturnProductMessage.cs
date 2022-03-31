namespace AurSystem.Framework.Messages;

public interface ReturnProductMessage
{
    IList<ProductLine> Lines { get; }
}