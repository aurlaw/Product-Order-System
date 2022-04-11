using InventoryService.Api.Models.Dto;
using InventoryService.Api.Services;

namespace InventoryService.Api.EndpointDefinitions;

public class InventoryEndpoint  : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IInventoryService, Services.InventoryService>();
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/inventory", () => Results.Ok(new {IsRunning = true}))
            .WithName("GetInventory").WithTags("InventoryServiceAPI");
        // get product inventory
        app.MapGet("/inventory/product/{id}", async (Guid id, IInventoryService service,
                ILogger<Program> logger, CancellationToken cancellationToken) =>
            {
                if (id == Guid.Empty)
                {
                    return Results.Conflict("Invalid Product id");
                }

                var results = await service.GetStream(id, cancellationToken);
                return Results.Ok(results);
            })
            .Produces<InventoryDto>()
            .ProducesValidationProblem(StatusCodes.Status409Conflict)
            .WithName("GetProductInventoryById").WithTags("InventoryServiceAPI");
        // add product inventory
        app.MapPost("/inventory/product/add", async (InventoryEventDto inventory, IInventoryService service,
                ILogger<Program> logger, CancellationToken cancellationToken) =>
            {
                if (inventory.ProductId == Guid.Empty || inventory.Quantity <= 0)
                {
                    return Results.Conflict("Invalid Product Id and/or product quantity <= 0");
                }
                logger.LogInformation("Add inventory for product with id {ProductId} - {Quantity}", 
                    inventory.ProductId, inventory.Quantity);
                await service.AddInventoryAsync(inventory, cancellationToken);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status409Conflict)
            .WithName("AddProductInventory").WithTags("InventoryServiceAPI");
        // sub product inventory
        app.MapPost("/inventory/product/subtract", async (InventoryEventDto inventory, IInventoryService service,
                ILogger<Program> logger, CancellationToken cancellationToken) =>
            {
                if (inventory.ProductId == Guid.Empty || inventory.Quantity <= 0)
                {
                    return Results.Conflict("Invalid Product Id and/or product quantity <= 0");
                }
                logger.LogInformation("Subtract inventory for product with id {ProductId} - {Quantity}", 
                    inventory.ProductId, inventory.Quantity);

                await service.SubtractInventoryAsync(inventory, cancellationToken);
                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status409Conflict)
            .WithName("SubtractProductInventory").WithTags("InventoryServiceAPI");
        
    }
}
