using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;
using AutoMapper;
using CustomerService.Api.Services;

namespace CustomerService.Api.EndpointDefinitions;

public class CustomerEndpoint : AurSystem.Framework.IEndpointDefinition
{
    public void DefineServices(IServiceCollection services)
    {
        //add services for this endpoint
        services.AddSingleton<ICustomerService, Services.CustomerService>();
    }
    
    public void DefineEndpoints(WebApplication app)
    {
        // get all
        app.MapGet("/customers", async (ICustomerService service, IMapper mapper, 
                CancellationToken cancellationToken) =>
            {
                var result =await service.GetCustomersAsync(cancellationToken);
                return Results.Ok(mapper.Map<IEnumerable<CustomerDto>>(result));
            })
            .Produces<IEnumerable<CustomerDto>>()
            .WithName("GetCustomers").WithTags("CustomerServiceAPI");
        // get by id
        app.MapGet("/customers/{id}", async (Guid id, ICustomerService service, IMapper mapper, 
                CancellationToken cancellationToken) =>
            {
                var result = await service.GetCustomerByIdAsync(id, cancellationToken);
                return result is not null ? Results.Ok(mapper.Map<CustomerDto>(result)) : Results.NotFound();
            })
            .Produces<CustomerDto>()
            .ProducesProblem(404)
            .WithName("GetCustomerById").WithTags("CustomerServiceAPI");
        // update customer balance
        app.MapPost("/customers/balance", async (CustomerBalanceDto customerBalance, ICustomerService service,
                ILogger<Program> logger, CancellationToken cancellationToken) =>
            {
                if (customerBalance.Id == Guid.Empty || customerBalance.Balance < 0)
                {
                    return Results.Conflict("Customer balance does not contain an ID or balance >= 0");
                }
                try
                {
                    await service.UpdateBalanceAsync(customerBalance.Id, customerBalance.Balance, cancellationToken);
                    return Results.NoContent();
                }
                catch (NotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Customer Exception");
                    return Results.StatusCode(500);
                }
            })
            .Produces(204)
            .ProducesValidationProblem(409)
            .ProducesProblem(404)
            .WithName("UpdateCustomerBalance").WithTags("CustomerServiceAPI");
    }
}
