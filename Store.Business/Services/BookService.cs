using AutoMapper;
using Store.Business.Models.Books;
using Store.Business.Services.Interfaces;
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

        public async Task<int?> Create(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);

            return await _bookRepository.Create(book);
        }

        public async Task<int> Delete(int id)
        {
            return await _bookRepository.Delete(id);
        }

        public async Task<BookModel?> Get(int id)
        {
            var book = await _bookRepository.Get(id);

            var model = _mapper.Map<BookModel?>(book);

            return model;
        }

        public async Task<IEnumerable<BookModel>> Get()
        {
            var bookDtos = await _bookRepository.Get();

            var booksGrouped = bookDtos
                    .GroupBy(b => b.Id)
                    .Select(group => new Book
                    {
                        Id = group.Key,
                        Name = group.First().Name,
                        Description = group.First().Description,
                        Price = group.First().Price,
                        PublishedDate = group.First().PublishedDate,
                        Authors = group.GroupBy(a => a.AuthorId)
                        .Select(authorGroup => new Author
                            {
                                Id = authorGroup.Key,
                                FirstName = authorGroup.First().FirstName,
                                LastName = authorGroup.First().LastName
                            }).ToList(),
                        Categories = group.GroupBy(c => c.CategoryId)
                        .Select(categotyGroup => new Category
                        {
                            Id = categotyGroup.Key,
                            Name = categotyGroup.First().CategoryName,
                        }).ToList(),
                        BookDetails = group.GroupBy(d => d.DetailId)
                        .Select(detailGroup => new BookDetail
                        {
                            Id = detailGroup.Key,
                            Language = detailGroup.First().Language,
                            PageCount = detailGroup.First().PageCount,
                            Publisher = detailGroup.First().Publisher,
                        }).ToList()
                    });

            var models = _mapper.Map<IEnumerable<BookModel>>(booksGrouped);

            return models;
        }

        public async Task<BookModel?> Update(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);

            book = await _bookRepository.Update(book);

            var model = _mapper.Map<BookModel?>(book);

            return model;
        }
    }
}
