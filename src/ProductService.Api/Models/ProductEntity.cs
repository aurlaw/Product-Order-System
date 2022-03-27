using Postgrest.Attributes;
using Supabase;

namespace ProductService.Api.Models;

[Table("products")]
public class ProductEntity : SupabaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }
    
    [Column("product_name")]
    public string ProductName { get; set; } = null!;
    
    [Column("qty")]
    public int Qty { get; set; }

    [Column("price")]
    public double Price { get; set; }
        
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
  
    [Column("modified_at")]
    public DateTime? ModifiedAt { get; set; }
}
