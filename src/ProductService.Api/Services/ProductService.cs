using AurSystem.Framework.Exceptions;
using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Services;
using AutoMapper;
using Postgrest;
using ProductService.Api.Models;

namespace ProductService.Api.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly SupabaseClient _supabaseClient;
    private readonly IMapper _mapper;

    public ProductService(ILogger<ProductService> logger, SupabaseClient supabaseClient, IMapper mapper)
    {
        _logger = logger;
        _supabaseClient = supabaseClient;
        _mapper = mapper;
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken token = default)
    {
        var productData = await GetProductEntityByIdAsync(id, token);
        return productData is not null ? _mapper.Map<Product>(productData) : null;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token = default)
    {
        var client = await _supabaseClient.GetClient();
        var productData = await client.From<ProductEntity>()
            .Get();
        return productData?.Models != null
            ? _mapper.Map<List<Product>>(productData.Models)
            : Enumerable.Empty<Product>();

    }

    public async Task UpdateQtyAsync(Guid id, int quantity, CancellationToken token = default)
    {
        var existingModel = await GetProductEntityByIdAsync(id, token);
        if (existingModel is null)
        {
            throw new NotFoundException("Product", $"Product not found for id {id}");
        }

        existingModel.Qty = quantity;
        existingModel.ModifiedAt = DateTime.UtcNow;
        var client = await _supabaseClient.GetClient();
        await client.From<ProductEntity>().Update(existingModel);

    }
    private async Task<ProductEntity?> GetProductEntityByIdAsync(Guid id, CancellationToken token = default)
    {
        var client = await _supabaseClient.GetClient();
        var productData = await client.From<ProductEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Single();
        return productData;
    }
}