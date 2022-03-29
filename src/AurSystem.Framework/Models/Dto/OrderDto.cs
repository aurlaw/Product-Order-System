using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;

namespace AurSystem.Framework.Models.Dto;

public class OrderDto
{
    public Guid Id { get; set; }
  
    public string OrderNumber { get; set; } = null!;

    public Guid CustomerId { get; set; }
    
    public double Total { get; set; }
    
    [JsonConverter(typeof(SmartEnumNameConverter<OrderStatus,int>))]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItemDto> LineItems { get; set; } = new List<OrderItemDto>();

    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }    
    
}