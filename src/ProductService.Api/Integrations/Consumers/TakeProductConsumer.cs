using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Messages;
using AurSystem.Framework.Models.Domain;
using MassTransit;
using ProductService.Api.Exceptions;
using ProductService.Api.Services;

namespace ProductService.Api.Integrations.Consumers;

public class TakeProductConsumer : IConsumer<TakeProductMessage>
{
    private readonly ILogger<TakeProductConsumer> _logger;
    private readonly IProductService _productService;

    public TakeProductConsumer(ILogger<TakeProductConsumer> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task Consume(ConsumeContext<TakeProductMessage> context)
    {
        _logger.LogInformation("TakeProductConsumer -> total products to update {Count}", context.Message.Lines.Count);
        List<Product> updatedProducts = new();
        foreach (var productLine in context.Message.Lines)
        {
            _logger.LogInformation("Get product info for id {ProductId}", productLine.ProductId);
            var productInfo =
                await _productService.GetProductByIdAsync(productLine.ProductId, context.CancellationToken);
            if (productInfo is null)
            {
                throw new NotFoundException("Take Product Not Found",
                    $"Product info not found for id: {productLine.ProductId}");
            }
            // checking qty taken against current product qty
            _logger.LogInformation("Checking taken qty {Quantity} vs product qty {Qty}",
                productLine.Quantity, productInfo.Qty);
            if (productLine.Quantity > productInfo.Qty)
            {
                throw new QuantityException("Take Product Quantity Error", 
                    $"Current product inventory cannot satisfy order {productLine.Quantity}");
            }
            var updatedQty = productInfo.Qty - productLine.Quantity;
            // update product
            productInfo.Qty = updatedQty;
            productInfo.ModifiedAt = DateTime.UtcNow;
            updatedProducts.Add(productInfo);
        }
        // update db
        _logger.LogInformation("Take Batch update products - {Count}", updatedProducts.Count);
        await _productService.BatchUpdateQtyAsync(updatedProducts, context.CancellationToken);

        await context.RespondAsync<ProductResponse>(new {Result = 1});
    }
}