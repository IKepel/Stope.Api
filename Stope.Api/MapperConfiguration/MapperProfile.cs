using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Models.Products;
using Store.ViewModels.OrderViewModels;
using Store.ViewModels.ProductViewModels;

namespace Stope.Api.MapperConfiguration
{
    public class MapperProfileVM : Profile
    {
        public MapperProfileVM()
        {
            CreateMap<OrderModel, OrderViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));

            CreateMap<ProductModel, ProductViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1));
        }
    }
}
