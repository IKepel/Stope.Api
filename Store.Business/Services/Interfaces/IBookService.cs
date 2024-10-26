using Store.Business.Models.Books;
using Store.Data.Dtos;

namespace Store.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookModel?> Get(int id);

        Task<IEnumerable<BookModel>> Get();

        Task<int?> Create(BookModel bookModel);

        Task<BookModel?> Update(BookModel bookModel);

        Task<int> Delete(int id);
    }
}
