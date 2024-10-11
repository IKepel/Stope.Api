using AutoMapper;
using Store.Business.Models.Products;
using Store.Business.Services.Interfaces;
using Store.Data.Repositories.Iterfaces;

namespace Store.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductModel Get(int id)
        {
            var product = _productRepository.Get(id);

            var model = _mapper.Map<ProductModel>(product);

            return model;
        }
    }
}
