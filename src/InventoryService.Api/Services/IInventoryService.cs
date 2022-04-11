using InventoryService.Api.Models.Dto;

namespace InventoryService.Api.Services;

public interface IInventoryService
{
    Task AddInventoryAsync(InventoryDto inventory, CancellationToken token = default);
    Task SubtractInventoryAsync(InventoryDto inventory, CancellationToken token = default);
}