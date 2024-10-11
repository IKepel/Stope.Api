using Store.Data.Entities;

namespace Store.Data.Repositories.Iterfaces
{
    public interface IProductRepository
    {
        Product Get(int id);
        IEnumerable<Product> Get();
        int Create();
    }
}
