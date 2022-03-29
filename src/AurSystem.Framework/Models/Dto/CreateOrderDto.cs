using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class CreateOrderDto
{
    [Required]
    public Guid CustomerId { get; set; }
    
    public List<CreateOrderItemDto> LineItems { get; set; } = new List<CreateOrderItemDto>();
}