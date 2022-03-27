namespace AurSystem.Framework.Models.Domain;

public class Order
{
    public Guid Id { get; set; }
  
    public string OrderNumber { get; set; } = null!;

    public Guid CustomerId { get; set; }
    
    public double Total { get; set; }
    
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItems> LineItems { get;  } = new List<OrderItems>();

    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }    
    
}