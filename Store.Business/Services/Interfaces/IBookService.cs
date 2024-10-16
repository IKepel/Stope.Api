using Store.Business.Models.Products;
using Store.Data.Requests;

namespace Store.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookModel> Get(int id);

        Task<IEnumerable<BookModel>> Get();

        Task<int> Create(UpsertBookRequestModel bookModel);

        Task<BookModel> Update(UpsertBookRequestModel bookModel);

        Task<int> Delete(int id);

        //Task<int> Create(BookRequestModel book);
    }
}
