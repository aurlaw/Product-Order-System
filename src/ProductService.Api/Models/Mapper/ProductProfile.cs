using AurSystem.Framework.Models.Domain;
using AutoMapper;

namespace ProductService.Api.Models.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity, Product>()
            .ReverseMap();
    }
}