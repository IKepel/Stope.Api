using AutoMapper;
using Store.Data.Entities;
using Store.Business.Models.Books;
using Store.Business.Models.BookDetails;
using Store.Data.Dtos;
using Store.Business.Models.Orders;
using Store.Business.Models.OrderItems;

namespace Store.Business.MapperConfiguration
{
    public class MapperModelProfile : Profile
    {
        public MapperModelProfile()
        {
            CreateMap<OrderModel, Order>();
            CreateMap<OrderItemModel, OrderItem>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<Book, BookDto>();
            CreateMap<BookModel, Book>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.AuthorIds.Select(id => new Author { Id = id })))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryIds.Select(id => new Category { Id = id })));
            CreateMap<BookDetailModel, BookDetail>();
            CreateMap<Author, AuthorDto>();  
            CreateMap<Category, CategoryDto>();  
            CreateMap<BookDetail, BookDetailDto>();  
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
