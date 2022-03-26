namespace CustomerService.Api.EndpointDefinitions;

public class CustomerEndpoint : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        //add services for this endpoint
    }

    public void DefineEndpoints(WebApplication app)
    {
        // get all
        app.MapGet("/customers", (CancellationToken cancellationToken) => Results.Ok())
            .Produces(200)
            .WithName("GetCustomers").WithTags("CustomerServiceAPI");
        // get by id
        app.MapGet("/customers/{id}", (Guid id,  CancellationToken cancellationToken) => Results.Ok())
            .Produces(200)
            .ProducesProblem(404)
            .WithName("GetCustomerById").WithTags("CustomerServiceAPI");
    }
}