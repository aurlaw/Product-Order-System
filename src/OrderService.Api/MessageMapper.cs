using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api;

public class MessageMapper
{
    private readonly Dictionary<Type, string> _map = new();
    private readonly IEndpointNameFormatter _formatter;
    
    public MessageMapper(IEndpointNameFormatter formatter)
    {
        _formatter = formatter;
        _map.Add(typeof(ChargeCustomerMessage), "ChargeCustomer");
        _map.Add(typeof(CreditCustomerMessage), "CreditCustomer");
        _map.Add(typeof(TakeProductMessage), "TakeProduct");
        _map.Add(typeof(ReturnProductMessage), "ReturnProduct");
    }

    public string GetMessageName<T>()
    {
        var t = typeof(T);
        if (!_map.ContainsKey(t))
            throw new ArgumentOutOfRangeException(t.FullName);
        return _formatter.SanitizeName(_map[t]);
    }
}