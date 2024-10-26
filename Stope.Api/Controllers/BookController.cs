using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stope.Api.Models.Requests;
using Store.Business.Models.Books;
using Store.Business.Services.Interfaces;
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
        public async Task<int?> Create([FromBody] BookRequestModel book)
        {
            var model = _mapper.Map<BookModel>(book);

            return await _bookService.Create(model);
        }

        [HttpGet("{id}")]
        public async Task<BookViewModel?> Get(int id)
        {
            var model = await _bookService.Get(id);

            var viewModel = _mapper.Map<BookViewModel?>(model);

            if (viewModel != null)
            {
                viewModel.Status = 1;
            }

            return viewModel;
        }

        [HttpGet]
        public async Task<IEnumerable<BookViewModel>> Get()
        {
            var models = await _bookService.Get();

            var viewModels = _mapper.Map<IEnumerable<BookViewModel>>(models);

            foreach (var model in viewModels)
            {
                model.Status = 1;
            }

            return viewModels;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _bookService.Delete(id);
        }

        [HttpPut("{id}")]
        public async Task<BookViewModel> Update(int id, [FromBody] BookRequestModel bookModel)
        {
            bookModel.Id = id;
            var model = _mapper.Map<BookModel>(bookModel);

            var book = await _bookService.Update(model);

            var viewModel = _mapper.Map<BookViewModel>(book);
            viewModel.Status = 1;

            return viewModel;
        }
    }
}
