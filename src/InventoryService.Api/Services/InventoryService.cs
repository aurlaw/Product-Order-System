using AutoMapper;
using InventoryService.Api.Models.Dto;
using InventoryService.Api.Models.Events;

namespace InventoryService.Api.Services;

public class InventoryService : IInventoryService
{
    private readonly ILogger<InventoryService> _logger;
    private readonly IMapper _mapper;

    public InventoryService(ILogger<InventoryService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task AddInventoryAsync(InventoryDto inventory, CancellationToken token = default)
    {
        var @event = _mapper.Map<AddInventory>(inventory);
        _logger.LogInformation("Adding inventory: {Quantity} - {ProductId}", @event.Quantity, @event.ProductId);
        throw new NotImplementedException();
    }

    public async Task SubtractInventoryAsync(InventoryDto inventory, CancellationToken token = default)
    {
        var @event = _mapper.Map<SubtractInventory>(inventory);
        _logger.LogInformation("Subtract inventory: {Quantity} - {ProductId}", @event.Quantity, @event.ProductId);
        throw new NotImplementedException();
    }
}