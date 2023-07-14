using AutoMapper;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Models.Dto;

namespace Mango.Services.OrderAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();
            config.CreateMap<CartDetailsDto, OrderDetailDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Price));

            config.CreateMap<OrderDetailDto, CartDetailsDto>();
            
            config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            config.CreateMap<OrderDetailDto, OrderDetails>().ReverseMap();
        });
        return mappingConfig;
    }
}