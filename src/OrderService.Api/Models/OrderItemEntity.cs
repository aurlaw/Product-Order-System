using Postgrest.Attributes;
using Supabase;

namespace OrderService.Api.Models;

[Table("order_items")]
public class OrderItemEntity : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }
  
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }
    
    [Column("qty")]
    public int Qty { get; set; }

    [Column("total")]
    public double Total { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
  
    [Column("modified_at")]
    public DateTime? ModifiedAt { get; set; }    
    
}