
namespace AurSystem.Framework.Models.Domain;

public class Product
{
    public Guid Id { get; set; }
    
    public string ProductName { get; set; } = null!;
    
    public int Qty { get; set; }

    public double Price { get; set; }
        
    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }
}