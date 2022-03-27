namespace AurSystem.Framework.Models.Domain;

public class Customer
{
    public Guid Id { get; set; }
  
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
  
    public string EmailAddress { get; set; } = null!;
  
    public double Balance { get; set; }
  
    public DateTime CreatedAt { get; set; }
  
    public DateTime? ModifiedAt { get; set; }    
}