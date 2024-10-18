using Store.Business.Models.Books;
using Store.Data.Dtos;

namespace Store.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> Get(int id);

        Task<IEnumerable<BookDto>> Get();

        Task<int> Create(BookModel bookModel);

        Task<BookDto> Update(BookModel bookModel);

        Task<int> Delete(int id);
    }
}
