using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

namespace Store.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString = "";

        public Product Get(int id)
        {
            //db connection

            return new Product();
        }

        public IEnumerable<Product> Get()
        {
            return new List<Product>();
        }

        public int Create()
        {
            return 1;
        }
    }
}
