using AurSystem.Framework.Models.Domain;

namespace ProductService.Api.Services;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token = default);
    Task UpdateQtyAsync(Guid id, int quantity, CancellationToken token = default);
    Task BatchUpdateQtyAsync(IList<Product> productList, CancellationToken token = default);
    
}