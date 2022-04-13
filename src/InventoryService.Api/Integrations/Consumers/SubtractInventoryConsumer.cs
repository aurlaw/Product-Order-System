using AurSystem.Framework.Messages;
using InventoryService.Api.Models.Dto;
using InventoryService.Api.Services;
using MassTransit;

namespace InventoryService.Api.Integrations.Consumers;

public class SubtractInventoryConsumer : IConsumer<SubtractInventoryMessage>
{
    private readonly ILogger<SubtractInventoryConsumer> _logger;
    private readonly IInventoryService _inventoryService;

    public SubtractInventoryConsumer(ILogger<SubtractInventoryConsumer> logger, IInventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }
    
    public async Task Consume(ConsumeContext<SubtractInventoryMessage> context)
    {
        _logger.LogInformation("SubtractInventoryConsumer -> total products to update {Count}", context.Message.Lines.Count);

        // subtract inventory
        foreach (var productLine in context.Message.Lines)
        {
            _logger.LogInformation("Subtract inventory for product with id {ProductId} - {Quantity}", 
                productLine.ProductId, productLine.Quantity);
            var dto = new InventoryEventDto
            {
                ProductId = productLine.ProductId,
                Quantity = productLine.Quantity
            };
            await _inventoryService.SubtractInventoryAsync(dto, context.CancellationToken);
        }
        await context.RespondAsync<ProductResponse>(new {Result = 1});

    }
}