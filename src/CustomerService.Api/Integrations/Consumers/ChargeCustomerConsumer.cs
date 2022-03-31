using AurSystem.Framework.Messages;
using CustomerService.Api.Services;
using MassTransit;

namespace CustomerService.Api.Integrations.Consumers;

public class ChargeCustomerConsumer : IConsumer<ChargeCustomerMessage>
{
    private readonly ILogger<ChargeCustomerConsumer> _logger;
    private readonly ICustomerService _customerService;

    public ChargeCustomerConsumer(ILogger<ChargeCustomerConsumer> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    public async Task Consume(ConsumeContext<ChargeCustomerMessage> context)
    {
        throw new NotImplementedException();
    }
}

/*
    public class TakeProductTransactionConsumer : IConsumer<TakeProductTransactionMessage>
    {
        private readonly ILogger<TakeProductTransactionConsumer> logger;
        private readonly IProductService productService;
        private readonly IPublishEndpoint publishEndpoint;

        public TakeProductTransactionConsumer(ILogger<TakeProductTransactionConsumer> logger,
            IProductService productService,
            IPublishEndpoint publishEndpoint)
        {
            this.logger = logger;
            this.productService = productService;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<TakeProductTransactionMessage> context)
        {
            logger.LogInformation($"Take product called ");
         
            Dictionary<int, int> productCounts = new Dictionary<int, int>();
            foreach (var item in context.Message.ProductBaskets)
            {
                productCounts.Add(item.ProductId, item.Count);
            }
            var products = await productService.TakeProducts(productCounts);
            await publishEndpoint.Publish<ProductsUpdatedEvent>(new
            {
                ProductUpdatedEvents = products.Select(p =>new { ProductId = p.Id,p.Price,p.Count}).ToList()
            });

            await context.RespondAsync<IRequestResult>(new { Result = 1 });
        }
    } *
 * 
 */