using AurSystem.Framework.Messages;
using InventoryService.Api.Models.Dto;
using InventoryService.Api.Services;
using MassTransit;

namespace InventoryService.Api.Integrations.Consumers;

public class AddInventoryConsumer : IConsumer<AddInventoryMessage>
{
    private readonly ILogger<AddInventoryConsumer> _logger;
    private readonly IInventoryService _inventoryService;

    public AddInventoryConsumer(ILogger<AddInventoryConsumer> logger, IInventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }
    
    public async Task Consume(ConsumeContext<AddInventoryMessage> context)
    {
        _logger.LogInformation("AddInventoryConsumer -> total products to update {Count}", context.Message.Lines.Count);

        // add inventory
        foreach (var productLine in context.Message.Lines)
        {
            _logger.LogInformation("Add inventory for product with id {ProductId} - {Quantity}", 
                productLine.ProductId, productLine.Quantity);
            var dto = new InventoryEventDto
            {
                ProductId = productLine.ProductId,
                Quantity = productLine.Quantity
            };
            await _inventoryService.AddInventoryAsync(dto, context.CancellationToken);

        }
        await context.RespondAsync<ProductResponse>(new {Result = 1});
        
    }
}