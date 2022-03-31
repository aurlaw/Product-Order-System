using AurSystem.Framework.Messages;
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
        throw new NotImplementedException();
    }
}