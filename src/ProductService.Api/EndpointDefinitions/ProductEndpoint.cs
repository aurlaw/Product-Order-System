using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;
using AutoMapper;
using ProductService.Api.Services;

namespace ProductService.Api.EndpointDefinitions;

public class ProductEndpoint : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        //add services for this endpoint
        services.AddSingleton<IProductService, Services.ProductService>();
    }

    public void DefineEndpoints(WebApplication app)
    {
        // get all
        app.MapGet("/products", async (IProductService service, IMapper mapper, 
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetProductsAsync(cancellationToken);
                return Results.Ok(mapper.Map<IEnumerable<ProductDto>>(result));
            })
            .Produces<IEnumerable<ProductDto>>()
            .WithName("GetProducts").WithTags("ProductServiceAPI");
        // get by id
        app.MapGet("/products/{id}", async (Guid id, IProductService service, IMapper mapper, 
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetProductByIdAsync(id, cancellationToken);
                return result is not null ? Results.Ok(mapper.Map<ProductDto>(result)) : Results.NotFound();
            })
            .Produces<ProductDto>()
            .ProducesProblem(404)
            .WithName("GetProductById").WithTags("ProductServiceAPI");    
        // update product quantity
        app.MapPost("/products/balance", async (ProductQtyDto productQty, IProductService service, 
            ILogger<Program> logger, CancellationToken cancellationToken) =>
        {
            if (productQty.Id == Guid.Empty || productQty.Qty < 0)
            {
                return Results.Conflict("Product Quantity does not contain an ID or qty >= 0");
            }
            try
            {
                await service.UpdateQtyAsync(productQty.Id, productQty.Qty, cancellationToken);
                return Results.NoContent();
            }
            catch (NotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Product Exception");
                return Results.StatusCode(500);
            }
        })
            .Produces(204)
            .ProducesValidationProblem(409)
            .ProducesProblem(404)
            .WithName("UpdateProductQty").WithTags("ProductServiceAPI");
    }
}
