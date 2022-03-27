using AurSystem.Framework.Models.Domain;
using AurSystem.Framework.Models.Dto;
using AutoMapper;

namespace CustomerService.Api.Models.Mapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerEntity>()
            .ReverseMap();
        CreateMap<Customer, CustomerDto>()
            .ReverseMap();
    }
}
