using InventoryService.Api.Models.Dto;
using InventoryService.Api.Models.Events;
using AutoMapper;

namespace InventoryService.Api.Models.Mapper;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<AddInventory, InventoryEventDto>()
            .ReverseMap();
        CreateMap<SubtractInventory, InventoryEventDto>()
            .ReverseMap();
        CreateMap<Inventory, InventoryDto>()
            .ForMember(d => d.ProductId, 
                opt => 
                    opt.MapFrom(src => src.Id))
            .ForMember(d => d.Modified, opt =>
                opt.MapFrom(src => src.Timestamp.DateTime))
            .ReverseMap();
    }   
}
// .ForMember(d => d.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))