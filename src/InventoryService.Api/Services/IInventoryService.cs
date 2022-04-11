using InventoryService.Api.Models.Dto;

namespace InventoryService.Api.Services;

public interface IInventoryService
{
    Task AddInventoryAsync(InventoryEventDto inventoryEvent, CancellationToken token = default);
    Task SubtractInventoryAsync(InventoryEventDto inventoryEvent, CancellationToken token = default);
    Task<InventoryDto?> GetStream(Guid productId, CancellationToken token = default);
}