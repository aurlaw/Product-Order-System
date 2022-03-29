using AurSystem.Framework.Models;
using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;
using AutoMapper;
using OrderService.Api.Services;

namespace OrderService.Api.EndpointDefinitions;

public class OrderEndpoint : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        //add services for this endpoint
        services.AddSingleton<IOrderService, Services.OrderService>();
    }

    public void DefineEndpoints(WebApplication app)
    {
        // get all
        app.MapGet("/orders", async (IOrderService service, IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetOrdersAsync(cancellationToken);
                return Results.Ok(mapper.Map<IEnumerable<OrderDto>>(result));
            })
            .Produces<IEnumerable<OrderDto>>()
            .WithName("GetOrders").WithTags("OrderServiceAPI");
        // get by id
        app.MapGet("/orders/{id}", async (Guid id, IOrderService service, IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetOrderByIdAsync(id, cancellationToken);
                return result is not null ? Results.Ok(mapper.Map<OrderDto>(result)) : Results.NotFound();
            })
            .Produces<OrderDto>()
            .ProducesProblem(404)
            .WithName("GetOrderById").WithTags("OrderServiceAPI");
        // create order
        app.MapPost("/orders/create", async (CreateOrderDto orderDto, IOrderService service, 
                IMapper mapper, ILogger<Program> logger, CancellationToken cancellationToken) =>
            {
                logger.LogInformation("Create order domain from DTO");
                var newOrder = OrderFactory.Create(orderDto);
                logger.LogInformation("Order domain created with order number: {OrderNumber} - Total Items: {Count} Amount: {Total:C}", 
                    newOrder.OrderNumber, newOrder.LineItems.Count, newOrder.Total);

                var createOrder = await service.CreateOrder(newOrder, cancellationToken);
                return Results.Ok(mapper.Map<OrderDto>(createOrder));
            })
            .Produces<OrderDto>()
            .ProducesValidationProblem(409)
            .WithName("CreateOrder").WithTags("OrderServiceAPI");
    }
}
/*
 
Customer: 
    7ebe8773-9419-48be-bd04-e0a6a3634b75
ProductID:
    1569b096-1448-4360-9436-c7d4acbee0e1 (9.99)
    1c76d2db-2e2f-4a35-b1cd-84d4479804b7 (19.99)
    f05a0926-1831-4d27-af52-a14c6c289227 (29.95)
    
{
  "customerId": "7ebe8773-9419-48be-bd04-e0a6a3634b75",
  "lineItems": [
    {
      "productId": "1569b096-1448-4360-9436-c7d4acbee0e1",
      "qty": 10,
      "price": 9.99
    },
    {
      "productId": "1c76d2db-2e2f-4a35-b1cd-84d4479804b7",
      "qty": 10,
      "price": 19.99
    },
    {
      "productId": "f05a0926-1831-4d27-af52-a14c6c289227",
      "qty": 10,
      "price": 29.95
    }
    
  ]
}    
    
    
 */