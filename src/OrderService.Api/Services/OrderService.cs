using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Services;
using AutoMapper;
using OrderService.Api.Models;
using Postgrest;

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
        var client = await _supabaseClient.GetClient();
        var orderData = await client.From<OrderEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Single();
        if (orderData is null) return null;
        var order = _mapper.Map<Order>(orderData);
        var orderItems = await GetMappedOrderItemsForOrder(order.Id, token);
        var enumerable = orderItems as OrderItem[] ?? orderItems.ToArray();
        if(enumerable.Any())
            order.LineItems.AddRange(enumerable);
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken token = default)
    {
        var client = await _supabaseClient.GetClient();
        var orderData = await client.From<OrderEntity>()
            .Get();
        var list = new List<Order>();
        if (orderData?.Models is null) return list;
        foreach (var order in orderData.Models.Select(orderEntity => _mapper.Map<Order>(orderEntity)))
        {
            var orderItems = await GetMappedOrderItemsForOrder(order.Id, token);
            var enumerable = orderItems as OrderItem[] ?? orderItems.ToArray();
            if(enumerable.Any())
                order.LineItems.AddRange(enumerable);
            list.Add(order);
        }
        return list;
    }

    public async Task<Order> CreateOrder(Order order, CancellationToken token = default)
    {
        var client = await _supabaseClient.GetClient();

        var orderEntity = _mapper.Map<OrderEntity>(order);
        orderEntity.CreatedAt = DateTime.UtcNow;
        orderEntity.ModifiedAt = DateTime.UtcNow;
        // insert order
        var newOrderData = await client.From<OrderEntity>()
            .Insert(orderEntity);
        var newOrder = _mapper.Map<Order>(newOrderData.Models.First());
        
        // insert order items
        var orderItems = _mapper.Map<List<OrderItemEntity>>(order.LineItems);
        if (!orderItems.Any()) return newOrder;
        orderItems.ForEach(o =>
        {
            o.OrderId = newOrder.Id;
            o.CreatedAt = DateTime.UtcNow;
            o.ModifiedAt = DateTime.UtcNow;
        });
        var newOrderItemsData = await client.From<OrderItemEntity>()
            .Insert(orderItems);
        var newOrderItems = _mapper.Map<List<OrderItem>>(newOrderItemsData.Models);
        newOrder.LineItems.AddRange(newOrderItems);
        return newOrder;
    }
    
    private async Task<IEnumerable<OrderItemEntity>> GetOrderItemsForOrder(Guid orderId, CancellationToken token)
    {
        var client = await _supabaseClient.GetClient();
        var orderItemData = await client.From<OrderItemEntity>()
            .Filter("order_id", Constants.Operator.Equals, orderId.ToString())
            .Get();
        return orderItemData?.Models ?? Enumerable.Empty<OrderItemEntity>();
    }
    private async Task<IEnumerable<OrderItem>> GetMappedOrderItemsForOrder(Guid orderId, CancellationToken token)
    {
        var orderItemData = await GetOrderItemsForOrder(orderId, token);
        return _mapper.Map<IEnumerable<OrderItem>>(orderItemData);
    }
    
}