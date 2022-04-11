using InventoryService.Api.Models.Events;

namespace InventoryService.Api.Models;

public class Inventory
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }

    public void Apply(AddInventory addInventory) => Quantity += addInventory.Quantity;
    public void Apply(SubtractInventory subtractInventory) => Quantity -= subtractInventory.Quantity;
}
