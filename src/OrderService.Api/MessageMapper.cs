using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api;

public class MessageMapper
{
    private Dictionary<Type, string> _map = new();
    private readonly IEndpointNameFormatter formatter;
    
    public MessageMapper(IEndpointNameFormatter formatter)
    {
        this.formatter = formatter;
        _map.Add(typeof(ChargeCustomerMessage), "ChargeCustomer");
        _map.Add(typeof(CreditCustomerMessage), "CreditCustomer");
        _map.Add(typeof(TakeProductMessage), "TakeProduct");
        _map.Add(typeof(ReturnProductMessage), "ReturnProduct");
    }

    public string GetMessageName<T>()
    {
        var t = typeof(T);
        if (_map.ContainsKey(t))
            throw new ArgumentOutOfRangeException(nameof(T));
        return formatter.SanitizeName(_map[t]);
    }
}