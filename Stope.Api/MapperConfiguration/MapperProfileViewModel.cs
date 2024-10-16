using AutoMapper;
using Store.Business.Models.Orders;
using Store.Business.Models.Products;
using Store.ViewModels.OrderViewModels;
using Store.ViewModels.ProductViewModels;

namespace Stope.Api.MapperConfiguration
{
    public class MapperViewModelProfile : Profile
    {
        public MapperViewModelProfile()
        {
            CreateMap<OrderModel, OrderViewModel>();

            CreateMap<BookModel, BookViewModel>();
        }
    }
}
