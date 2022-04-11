using AurSystem.Framework;
using AurSystem.Framework.Messages;
using MassTransit;

namespace ProductService.Api;

public class MessageMapper : MessageBaseMapper
{
    public MessageMapper(IEndpointNameFormatter formatter) : base(formatter)
    {
        _map.Add(typeof(TakeInventoryMessage), "TakeInventory");
        _map.Add(typeof(ReturnInventoryMessage), "ReturnInventory");
    }
}