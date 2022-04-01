using ApplicationException = AurSystem.Framework.Exceptions.ApplicationException;

namespace ProductService.Api.Exceptions;

public class QuantityException : ApplicationException
{
    public QuantityException(string title, string message) : base(title, message)
    {
    }
}