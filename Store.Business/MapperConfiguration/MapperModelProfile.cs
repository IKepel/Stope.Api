using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Models.Products;
using Store.Data.Entities;
using Store.Data.Requests;

namespace Store.Business.MapperConfiguration
{
    public class MapperModelProfile : Profile
    {
        public MapperModelProfile()
        {
            CreateMap<Order, OrderModel>();
            CreateMap<Book, BookModel>();
            CreateMap<UpsertBookRequestModel, Book>();
            CreateMap<UpsertOrderRequestModel, Order>();
                //.ForMember(dest => dest.PriceUah, opt => opt.MapFrom(src => src.Price))
                //.ForMember(dest => dest.PriceUsd, opt => opt.MapFrom(src => src.Price * 41.5))
                //.ForMember(dest => dest.PriceEuro, opt => opt.MapFrom(src => src.Price * 45))
        }
    }
}
