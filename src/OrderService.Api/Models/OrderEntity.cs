using Postgrest.Attributes;
using Supabase;

namespace OrderService.Api.Models;


[Table("orders")]
public class OrderEntity : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }
  
    [Column("order_number")]
    public string OrderNumber { get; set; } = null!;

    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    
    [Column("total")]
    public double Total { get; set; }
    
    [Column("status")]
    public string Status { get; set; } = null!;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
  
    [Column("modified_at")]
    public DateTime? ModifiedAt { get; set; }    

}