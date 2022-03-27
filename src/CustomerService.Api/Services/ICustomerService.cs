using AurSystem.Framework.Models.Domain;

namespace CustomerService.Api.Services;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken token =default);
    Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken token = default);
    Task UpdateBalanceAsync(Guid id, double balance, CancellationToken token = default);
}