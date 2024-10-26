using AutoMapper;
using Store.Data.Entities;
using Store.Business.Models.Books;
using Store.Business.Models.BookDetails;
using Store.Data.Dtos;
using Store.Business.Models.Orders;
using Store.Business.Models.OrderItems;
using Store.Business.Models.Categories;
using Store.Business.Models.Authors;
using Store.Business.Models.Users;

namespace Store.Business.MapperConfiguration
{
    public class MapperModelProfile : Profile
    {
        public MapperModelProfile()
        {
            CreateMap<Order, OrderModel>();
            CreateMap<User, UserModel>();
            CreateMap<OrderItem, OrderItemModel>();

            CreateMap<OrderItemModel, OrderItem>();
            CreateMap<OrderModel, Order>();



            CreateMap<Book, BookModel>();
            CreateMap<BookDetail, BookDetailModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Author, AuthorModel>();

            CreateMap<BookModel, Book>();
            CreateMap<BookDetailModel, BookDetail>();
            CreateMap<CategoryModel, Category>();
            CreateMap<AuthorModel, Author>();
            


            //.ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.AuthorIds.Select(id => new Author { Id = id })))
            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new Category { Id = id })));
            //    CreateMap<BookDetailModel, BookDetail>();
            //    CreateMap<Author, AuthorDto>();  
            //    CreateMap<Category, CategoryDto>();  
            //    CreateMap<BookDetail, BookDetailDto>();  
            //    CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
