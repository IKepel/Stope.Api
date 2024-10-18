using AutoMapper;
using Store.Business.Models.Books;
using Store.Business.Services.Interfaces;
using Store.Data.Dtos;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

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

        public async Task<int> Create(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);

            return await _bookRepository.Create(book);
        }

        public async Task<int> Delete(int id)
        {
            return await _bookRepository.Delete(id);
        }

        public async Task<BookDto> Get(int id)
        {
            var book = await _bookRepository.Get(id);

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;
        }

        public async Task<IEnumerable<BookDto>> Get()
        {
            var bookList = await _bookRepository.Get();

            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(bookList);

            return bookDtos;
        }

        public async Task<BookDto> Update(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);

            book = await _bookRepository.Update(book);

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;
        }
    }
}
