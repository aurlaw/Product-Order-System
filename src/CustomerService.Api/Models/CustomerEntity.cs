using Postgrest.Attributes;
using Supabase;

namespace CustomerService.Api.Models;

[Table("customers")]
public class CustomerEntity : SupabaseModel
{
 [PrimaryKey("id", false)]
  public Guid Id { get; set; }
  
  [Column("first_name")]
  public string FirstName { get; set; } = null!;

  [Column("last_name")]
  public string LastName { get; set; } = null!;
  
  [Column("email_address")]
  public string EmailAddress { get; set; } = null!;
  
  [Column("balance")]
  public double Balance { get; set; }
  
  [Column("created_at")]
  public DateTime CreatedAt { get; set; }
  
  [Column("modified_at")]
  public DateTime? ModifiedAt { get; set; }
  
}
