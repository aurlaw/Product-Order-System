using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Services;
using AutoMapper;
using CustomerService.Api.Models;
using Microsoft.Extensions.Options;
using Postgrest;

namespace CustomerService.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly SupabaseClient _supabaseClient;
    private readonly IMapper _mapper;

    public CustomerService(ILogger<CustomerService> logger, SupabaseClient supabaseClient, IMapper mapper)
    {
        _logger = logger;
        _supabaseClient = supabaseClient;
        _mapper = mapper;
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken token = default)
    {
        var customerData = await GetCustomerEntityByIdAsync(id, token);
        return customerData is not null ? _mapper.Map<Customer>(customerData) : null;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken token = default)
    {
        var client = await _supabaseClient.GetClient();
        var customerData = await client.From<CustomerEntity>()
            .Get();
        return customerData?.Models != null ? _mapper.Map<List<Customer>>(customerData.Models) : Enumerable.Empty<Customer>();
    }

    public async Task UpdateBalanceAsync(Guid id, double balance, CancellationToken token = default)
    {
        var existingModel = await GetCustomerEntityByIdAsync(id, token);
        if (existingModel is null)
        {
            throw new NotFoundException("Customer", $"Customer not found for id {id}");
        }
        existingModel.Balance = balance;
        existingModel.ModifiedAt = DateTime.UtcNow;
        var client = await _supabaseClient.GetClient();
        await client.From<CustomerEntity>().Update(existingModel);
    }
    private async Task<CustomerEntity?> GetCustomerEntityByIdAsync(Guid id, CancellationToken token = default)
    {
        var client = await _supabaseClient.GetClient();
        var customerData = await client.From<CustomerEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Single();
        return customerData;
    }
    
}
