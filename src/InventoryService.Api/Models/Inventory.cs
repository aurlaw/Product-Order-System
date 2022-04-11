using InventoryService.Api.Models.Events;

namespace InventoryService.Api.Models;

public class Inventory
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public long Version { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;

    public void Apply(AddInventory addInventory)
    {
        Quantity += addInventory.Quantity;
        Modified = DateTime.UtcNow;
        Version++;
    }

    public void Apply(SubtractInventory subtractInventory)
    {
        Quantity -= subtractInventory.Quantity;
        Modified = DateTime.UtcNow;
        Version++;
    }
}
