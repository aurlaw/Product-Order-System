using MassTransit;
using ProductService.Api.Services;

namespace ProductService.Api.Integrations.Consumers;

public class TakeProductConsumer : IConsumer<TakeProductConsumer>
{
    private readonly ILogger<TakeProductConsumer> _logger;
    private readonly IProductService _productService;

    public TakeProductConsumer(ILogger<TakeProductConsumer> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    
    public async Task Consume(ConsumeContext<TakeProductConsumer> context)
    {
        throw new NotImplementedException();
    }
}