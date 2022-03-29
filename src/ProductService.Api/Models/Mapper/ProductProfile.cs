using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;
using AutoMapper;

namespace ProductService.Api.Models.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductEntity, Product>()
            .ReverseMap();
        CreateMap<Product, ProductDto>()
            .ReverseMap();
    }
}