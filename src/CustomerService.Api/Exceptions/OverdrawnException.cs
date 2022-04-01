using ApplicationException = AurSystem.Framework.Exceptions.ApplicationException;

namespace CustomerService.Api.Exceptions;

public class OverdrawnException : ApplicationException
{
    public OverdrawnException(string title, string message) : base(title, message)
    {
    }
}