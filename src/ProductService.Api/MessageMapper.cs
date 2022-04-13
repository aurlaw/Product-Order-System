using AurSystem.Framework;
using AurSystem.Framework.Messages;
using MassTransit;

namespace ProductService.Api;

public class MessageMapper : MessageBaseMapper
{
    public MessageMapper(IEndpointNameFormatter formatter) : base(formatter)
    {
        _map.Add(typeof(SubtractInventoryMessage), "SubtractInventory");
        _map.Add(typeof(AddInventoryMessage), "AddInventory");
    }
}