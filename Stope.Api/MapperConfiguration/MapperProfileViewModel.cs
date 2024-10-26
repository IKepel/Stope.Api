using AutoMapper;
using Stope.Api.Models.Requests;
using Store.Business.Models.Authors;
using Store.Business.Models.BookDetails;
using Store.Business.Models.Books;
using Store.Business.Models.Categories;
using Store.Business.Models.OrderItems;
using Store.Business.Models.Orders;
using Store.Business.Models.Users;
using Store.ViewModels.AuthorViewModels;
using Store.ViewModels.BookDetailViewModels;
using Store.ViewModels.CategoryViewModels;
using Store.ViewModels.OrderItemViewModels;
using Store.ViewModels.OrderViewModels;
using Store.ViewModels.ProductViewModels;
using Store.ViewModels.UserViewModels;

namespace Stope.Api.MapperConfiguration
{
    public class MapperViewModelProfile : Profile
    {
        public MapperViewModelProfile()
        {
            CreateMap<OrderRequestModel, OrderModel>();
            CreateMap<OrderItemRequestModel, OrderItemModel>();

            CreateMap<OrderModel, OrderViewModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<OrderItemModel, OrderItemViewModel>();

            CreateMap<BookRequestModel, BookModel>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.AuthorIds.Select(id => new AuthorModel { Id = id })))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new CategoryModel { Id = id })));

            CreateMap<BookDetailRequestModel, BookDetailModel>();

            CreateMap<BookModel, BookViewModel>();
            CreateMap<AuthorModel, AuthorViewModel>();  
            CreateMap<CategoryModel, CategoryViewModel>();  
            CreateMap<BookDetailModel, BookDetailViewModel>();  
        }
    }
}
