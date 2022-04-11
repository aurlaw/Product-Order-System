namespace InventoryService.Api.Models.Events;

public record SubtractInventory(Guid ProductId, int Quantity);