using AurSystem.Framework.Messages;
using MassTransit;

namespace AurSystem.Framework;

public static class FutureContractExtensions
{
    public static string GetExceptionMessages(this Fault faulted)
    {
        return faulted.Exceptions != null ? string.Join(Environment.NewLine, faulted.Exceptions.Select(x => x.Message)) : string.Empty;
    }

    public static string GetExceptionMessages(this FutureFaulted faulted)
    {
        return faulted.Exceptions != null ? string.Join(Environment.NewLine, faulted.Exceptions.Select(x => x.Message)) : string.Empty;
    }

    public static string GetExceptionMessages<T>(this Response<T> faulted)
        where T : class, FutureFaulted
    {
        return faulted.Message.Exceptions != null ? string.Join(Environment.NewLine, faulted.Message.Exceptions.Select(x => x.Message)) : string.Empty;
    }

    public static string GetExceptionMessages(this ExceptionInfo[] exceptionInfo)
    {
        return exceptionInfo.Any() ? string.Join(Environment.NewLine, exceptionInfo.Select(x => x.Message)) : string.Empty;
        
    }
}