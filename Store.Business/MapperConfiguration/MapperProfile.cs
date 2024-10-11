using AutoMapper;
using Store.Business.Models.Orders;
using Store.Data.Entities;

namespace Store.Business.MapperConfiguration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.PriceUah, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.PriceUsd, opt => opt.MapFrom(src => src.Price * 41.5))
                .ForMember(dest => dest.PriceEuro, opt => opt.MapFrom(src => src.Price * 45));
        }
    }
}
