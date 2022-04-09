using InventoryService.Api.Services;

namespace InventoryService.Api.EndpointDefinitions;

public class InventoryEndpoint  : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IInventoryService, Services.InventoryService>();
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/inventory", () => Results.Ok(new {IsRunning = true}))
            .WithName("GetInventory").WithTags("InventoryServiceAPI");
    }
}