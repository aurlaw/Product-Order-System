using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class ProductDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string ProductName { get; set; } = null!;
    
    [Required]
    public int Qty { get; set; }

    [Required]
    public double Price { get; set; }
        
    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }
    
}