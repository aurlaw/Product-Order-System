namespace OrderService.Api.EndpointDefinitions;

public class OrderEndpoint : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        //add services for this endpoint
    }

    public void DefineEndpoints(WebApplication app)
    {
        // get all
        app.MapGet("/orders", (CancellationToken cancellationToken) => Results.Ok())
            .Produces(200)
            .WithName("GetOrders").WithTags("OrderServiceAPI");
        // get by id
        app.MapGet("/orders/{id}", (Guid id,  CancellationToken cancellationToken) => Results.Ok())
            .Produces(200)
            .ProducesProblem(404)
            .WithName("GetOrderById").WithTags("OrderServiceAPI");
    }
}