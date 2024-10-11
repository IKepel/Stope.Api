using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

namespace Store.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString = "";

        public Order Get(int id)
        {
            //db connection

            return new Order()
            {
                Id = 1,
                Price = 469,
                ProductId = 5
            };
        }

        public IEnumerable<Order> Get()
        {
            return new List<Order>();
        }

        public int Create()
        {
            return 1;
        }
    }
}
