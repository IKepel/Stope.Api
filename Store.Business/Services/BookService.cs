using AutoMapper;
using Store.Business.Models.Products;
using Store.Business.Services.Interfaces;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;
using Store.Data.Requests;

namespace Store.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(UpsertBookRequestModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            return await _bookRepository.Create(book);
        }

        public async Task<int> Delete(int id)
        {
            return await _bookRepository.Delete(id);
        }

        public async Task<BookModel> Get(int id)
        {
            var book = await _bookRepository.Get(id);

            var model = _mapper.Map<BookModel>(book);

            return model;
        }

        public async Task<IEnumerable<BookModel>> Get()
        {
            var bookList = await _bookRepository.Get();

            var models = _mapper.Map<IEnumerable<BookModel>>(bookList);

            return models;
        }

        public async Task<BookModel> Update(UpsertBookRequestModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);

            book = await _bookRepository.Update(book);

            var model = _mapper.Map<BookModel>(book);

            return model;
        }
    }
}
