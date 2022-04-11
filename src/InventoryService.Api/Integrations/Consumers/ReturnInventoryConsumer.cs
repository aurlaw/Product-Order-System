using AurSystem.Framework.Messages;
using InventoryService.Api.Services;
using MassTransit;

namespace InventoryService.Api.Integrations.Consumers;

public class ReturnInventoryConsumer : IConsumer<ReturnInventoryMessage>
{
    private readonly ILogger<ReturnInventoryConsumer> _logger;
    private readonly IInventoryService _inventoryService;

    public ReturnInventoryConsumer(ILogger<ReturnInventoryConsumer> logger, IInventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }
    
    public async Task Consume(ConsumeContext<ReturnInventoryMessage> context)
    {
        _logger.LogInformation("ReturnInventoryConsumer -> total products to update {Count}", context.Message.Lines.Count);

        // add inventory
        foreach (var productLine in context.Message.Lines)
        {
            _logger.LogInformation("Add inventory for product with id {ProductId} - {Quantity}", 
                productLine.ProductId, productLine.Quantity);
        }
        await context.RespondAsync<ProductResponse>(new {Result = 1});
        
    }
}