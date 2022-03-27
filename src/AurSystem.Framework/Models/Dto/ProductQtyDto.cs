using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class ProductQtyDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public int Qty { get; set; }
    
}