using InventoryService.Api.Models.Events;

namespace InventoryService.Api.Models;

public class Inventory
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public long Version { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    
    public void Apply(AddInventory addInventory)
    {
        Quantity += addInventory.Quantity;
        Timestamp = addInventory.TimeStamp;
        Version++;
    }

    public void Apply(SubtractInventory subtractInventory)
    {
        Quantity -= subtractInventory.Quantity;
        Timestamp = subtractInventory.TimeStamp;
        Version++;
    }
}
