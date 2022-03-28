using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Services;
using AutoMapper;

namespace OrderService.Api.Services;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly SupabaseClient _supabaseClient;
    private readonly IMapper _mapper;

    public OrderService(ILogger<OrderService> logger, SupabaseClient supabaseClient, IMapper mapper)
    {
        _logger = logger;
        _supabaseClient = supabaseClient;
        _mapper = mapper;
    }
    
    public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}