namespace AurSystem.Framework.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string title, string message) : base(title, message)
    {
    }
}