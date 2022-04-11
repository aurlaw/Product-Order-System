using MassTransit;

namespace AurSystem.Framework;

public abstract class MessageBaseMapper
{
    protected readonly Dictionary<Type, string> _map = new();
    protected readonly IEndpointNameFormatter _formatter;

    protected MessageBaseMapper(IEndpointNameFormatter formatter)
    {
        _formatter = formatter;
    }
    
    public string GetMessageName<T>()
    {
        var t = typeof(T);
        if (!_map.ContainsKey(t))
            throw new ArgumentOutOfRangeException(t.FullName);
        return _formatter.SanitizeName(_map[t]);
    }
}