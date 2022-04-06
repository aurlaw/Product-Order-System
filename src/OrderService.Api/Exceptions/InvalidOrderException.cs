using ApplicationException = AurSystem.Framework.Exceptions.ApplicationException;

namespace OrderService.Api.Exceptions;

public class InvalidOrderException : ApplicationException
{
    public InvalidOrderException(string title, string message) : base(title, message)
    {
    }
}