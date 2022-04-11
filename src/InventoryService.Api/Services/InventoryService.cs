using AutoMapper;
using InventoryService.Api.Models;
using InventoryService.Api.Models.Dto;
using InventoryService.Api.Models.Events;
using Marten;
using Marten.Events;

namespace InventoryService.Api.Services;

public class InventoryService : IInventoryService
{
    private readonly ILogger<InventoryService> _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentSession _session;

    public InventoryService(ILogger<InventoryService> logger, IMapper mapper, IDocumentSession session)
    {
        _logger = logger;
        _mapper = mapper;
        _session = session;
    }
    
    public async Task AddInventoryAsync(InventoryEventDto inventoryEvent, CancellationToken token = default)
    {
        var @event = _mapper.Map<AddInventory>(inventoryEvent);
        var streamId = @event.ProductId;
        _logger.LogInformation("Adding inventoryEvent: {Quantity} - {streamId}", @event.Quantity, streamId);
        
        await Append(streamId, @event);
        
        await _session.SaveChangesAsync(token);
    }

    public async Task SubtractInventoryAsync(InventoryEventDto inventoryEvent, CancellationToken token = default)
    {
        var @event = _mapper.Map<SubtractInventory>(inventoryEvent);
        var streamId = @event.ProductId;
        _logger.LogInformation("Subtract inventoryEvent: {Quantity} - {streamId}", @event.Quantity, streamId);
        
        await Append(streamId, @event);
        
        await _session.SaveChangesAsync(token);
    }

    public async Task<InventoryDto?> GetStream(Guid productId, CancellationToken token = default)
    {
        var stream = await _session.Events.AggregateStreamAsync<Inventory>(productId, token: token);
        return stream is not null ? _mapper.Map<InventoryDto>(stream) : null;
    }


    private async Task Append(Guid streamId, object streamEvent)
    {
        var stream= await GetStream(streamId);
        if (stream is null)
        {
            _session.Events.StartStream<Inventory>(streamId, streamEvent);
        }
        else
        {
            _session.Events.Append(streamId, streamEvent);
        }
    }
}
