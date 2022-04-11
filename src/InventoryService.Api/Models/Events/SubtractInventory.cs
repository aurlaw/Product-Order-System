namespace InventoryService.Api.Models.Events;

public record SubtractInventory(Guid ProductId, int Quantity)
{
    public DateTimeOffset TimeStamp { get; init; } = DateTimeOffset.UtcNow;
}