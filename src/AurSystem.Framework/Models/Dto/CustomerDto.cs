using System.ComponentModel.DataAnnotations;

namespace AurSystem.Framework.Models.Dto;

public class CustomerDto
{
    [Required]
    public Guid Id { get; set; }
  
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
  
    [Required]
    public string EmailAddress { get; set; } = null!;
  
    [Required]
    public double Balance { get; set; }
  
    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }    
}