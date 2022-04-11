using InventoryService.Api.Models.Dto;
using InventoryService.Api.Models.Events;
using AutoMapper;

namespace InventoryService.Api.Models.Mapper;

public class InventoryProfile : Profile
{
    public InventoryProfile()
    {
        CreateMap<AddInventory, InventoryDto>()
            .ReverseMap();
        CreateMap<SubtractInventory, InventoryDto>()
            .ReverseMap();
    }   
}