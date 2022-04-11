using AurSystem.Framework;
using AurSystem.Framework.Messages;
using MassTransit;

namespace OrderService.Api;

public class MessageMapper : MessageBaseMapper
{
    
    public MessageMapper(IEndpointNameFormatter formatter) : base(formatter)
    {
        _map.Add(typeof(ChargeCustomerMessage), "ChargeCustomer");
        _map.Add(typeof(CreditCustomerMessage), "CreditCustomer");
        _map.Add(typeof(TakeProductMessage), "TakeProduct");
        _map.Add(typeof(ReturnProductMessage), "ReturnProduct");
    }

    // public string GetMessageName<T>()
    // {
    //     var t = typeof(T);
    //     if (!_map.ContainsKey(t))
    //         throw new ArgumentOutOfRangeException(t.FullName);
    //     return _formatter.SanitizeName(_map[t]);
    // }
}