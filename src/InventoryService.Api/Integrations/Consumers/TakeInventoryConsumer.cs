using AurSystem.Framework.Messages;
using InventoryService.Api.Services;
using MassTransit;

namespace InventoryService.Api.Integrations.Consumers;

public class TakeInventoryConsumer : IConsumer<TakeInventoryMessage>
{
    private readonly ILogger<TakeInventoryConsumer> _logger;
    private readonly IInventoryService _inventoryService;

    public TakeInventoryConsumer(ILogger<TakeInventoryConsumer> logger, IInventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }
    
    public async Task Consume(ConsumeContext<TakeInventoryMessage> context)
    {
        _logger.LogInformation("TakeInventoryConsumer -> total products to update {Count}", context.Message.Lines.Count);

        // subtract inventory
        foreach (var productLine in context.Message.Lines)
        {
            _logger.LogInformation("Subtract inventory for product with id {ProductId} - {Quantity}", 
                productLine.ProductId, productLine.Quantity);
        }
        await context.RespondAsync<ProductResponse>(new {Result = 1});

    }
}