using Store.Business.Models.Products;

namespace Store.Business.Services.Interfaces
{
    public interface IProductService
    {
        ProductModel Get(int id);
    }
}
