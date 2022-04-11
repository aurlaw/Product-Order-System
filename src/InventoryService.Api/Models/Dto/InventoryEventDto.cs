namespace InventoryService.Api.Models.Dto;

public class InventoryEventDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    
}