using AutoMapper;
using Stope.Api.Models.Requests;
using Store.Business.Models.BookDetails;
using Store.Business.Models.Books;
using Store.Business.Models.OrderItems;
using Store.Business.Models.Orders;
using Store.Data.Dtos;
using Store.ViewModels.AuthorViewModels;
using Store.ViewModels.BookDetailViewModels;
using Store.ViewModels.CategoryViewModels;
using Store.ViewModels.OrderItemViewModels;
using Store.ViewModels.OrderViewModels;
using Store.ViewModels.ProductViewModels;

namespace Stope.Api.MapperConfiguration
{
    public class MapperViewModelProfile : Profile
    {
        public MapperViewModelProfile()
        {
            CreateMap<OrderRequestModel, OrderModel>();
            CreateMap<OrderItemRequestModel, OrderItemModel>();

            CreateMap<OrderDto, OrderViewModel>(); 
            CreateMap<OrderItemDto, OrderItemViewModel>();

            CreateMap<BookRequestModel, BookModel>();
            CreateMap<BookDetailRequestModel, BookDetailModel>();

            CreateMap<BookDto, BookViewModel>();
            CreateMap<AuthorDto, AuthorViewModel>();  
            CreateMap<CategoryDto, CategoryViewModel>();  
            CreateMap<BookDetailDto, BookDetailViewModel>();  
            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}
