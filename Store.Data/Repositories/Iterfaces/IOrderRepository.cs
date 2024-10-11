using Store.Data.Entities;

namespace Store.Data.Repositories.Iterfaces
{
    public interface IOrderRepository
    {
        Order Get(int id);
        IEnumerable<Order> Get();
        int Create();
    }
}
