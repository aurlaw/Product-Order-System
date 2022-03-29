using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class CreateOrderItemDto
{
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    public int Qty { get; set; }

    [Required]
    public double Price { get; set; }
}