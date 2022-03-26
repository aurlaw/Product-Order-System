namespace ProductService.Api.EndpointDefinitions;

public class ProductEndpoint : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        //add services for this endpoint
    }

    public void DefineEndpoints(WebApplication app)
    {
        // get all
        app.MapGet("/products", (CancellationToken cancellationToken) => Results.Ok())
            .Produces(200)
            .WithName("GetProducts").WithTags("ProductServiceAPI");
        // get by id
        app.MapGet("/products/{id}", (Guid id,  CancellationToken cancellationToken) => Results.Ok())
            .Produces(200)
            .ProducesProblem(404)
            .WithName("GetProductById").WithTags("ProductServiceAPI");    
    }
}