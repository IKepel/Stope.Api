using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Services.Interfaces;
using Store.Data.Requests;
using Store.ViewModels.ProductViewModels;

namespace Stope.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<int> Create(UpsertBookRequestModel book)
        {
            return await _bookService.Create(book);
        }

        [HttpGet("{id}")]
        public async Task<BookViewModel> Get(int id)
        {
            var book = await _bookService.Get(id);

            var model = _mapper.Map<BookViewModel>(book);
            model.Status = 1;

            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<BookViewModel>> Get()
        {
            var bookList = await _bookService.Get();

            var models = _mapper.Map<IEnumerable<BookViewModel>>(bookList);

            foreach (var model in models)
            {
                model.Status = 1;
            }

            return models;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _bookService.Delete(id);
        }

        [HttpPut("[action]")]
        public async Task<BookViewModel> Update(UpsertBookRequestModel bookModel)
        {
            var book = await _bookService.Update(bookModel);

            var model = _mapper.Map<BookViewModel>(book);
            model.Status = 1;

            return model;
        }
    }
}
