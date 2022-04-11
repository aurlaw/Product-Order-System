namespace InventoryService.Api.Models.Events;

public record AddInventory(Guid ProductId, int Quantity)
{
    public DateTimeOffset TimeStamp { get; init; } = DateTimeOffset.UtcNow;
}
