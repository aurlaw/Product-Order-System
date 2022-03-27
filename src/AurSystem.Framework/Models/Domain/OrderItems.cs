namespace AurSystem.Framework.Models.Domain;

public class OrderItems
{
    public Guid Id { get; set; }
  
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }
    
    public int Qty { get; set; }

    public double Total { get; set; }

    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }    
    
}