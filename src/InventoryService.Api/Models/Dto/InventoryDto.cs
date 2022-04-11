namespace InventoryService.Api.Models.Dto;

public class InventoryDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public long Version { get; set; }
    public DateTime Modified { get; set; }

}