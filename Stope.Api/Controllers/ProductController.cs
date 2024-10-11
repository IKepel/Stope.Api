using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Services.Interfaces;
using Store.ViewModels.ProductViewModels;

namespace Stope.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("{id}")]
        public ProductViewModel Get(int id)
        {
            var product = _productService.Get(id);

            var model = _mapper.Map<ProductViewModel>(product);
            model.Status = 1;

            return model;
        }
    }
}
