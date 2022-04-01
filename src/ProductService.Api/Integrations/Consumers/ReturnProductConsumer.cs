using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Messages;
using AurSystem.Framework.Models.Domain;
using MassTransit;
using ProductService.Api.Services;

namespace ProductService.Api.Integrations.Consumers;

public class ReturnProductConsumer : IConsumer<ReturnProductMessage>
{
    private readonly ILogger<ReturnProductConsumer> _logger;
    private readonly IProductService _productService;

    public ReturnProductConsumer(ILogger<ReturnProductConsumer> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task Consume(ConsumeContext<ReturnProductMessage> context)
    {
        _logger.LogInformation("ReturnProductConsumer -> total products to update {Count}", context.Message.Lines.Count);
        List<Product> updatedProducts = new();
        foreach (var productLine in context.Message.Lines)
        {
            _logger.LogInformation("Get product info for id {ProductId}", productLine.ProductId);
            var productInfo =
                await _productService.GetProductByIdAsync(productLine.ProductId, context.CancellationToken);
            if (productInfo is null)
            {
                throw new NotFoundException("Return Product Not Found",
                    $"Product info not found for id: {productLine.ProductId}");
            }
            var updatedQty = productInfo.Qty + productLine.Quantity;
            // update product
            productInfo.Qty = updatedQty;
            productInfo.ModifiedAt = DateTime.UtcNow;
            updatedProducts.Add(productInfo);
        }
        // update db
        _logger.LogInformation("Return Batch update products - {Count}", updatedProducts.Count);
        await _productService.BatchUpdateQtyAsync(updatedProducts, context.CancellationToken);

        await context.RespondAsync<ProductResponse>(new {Result = 1});
        
    }
}
