using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class SubmitOrderDto
{
    [Required]
    public Guid OrderId { get; set; }
    
}