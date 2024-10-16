using Store.Data.Entities;

namespace Store.Data.Repositories.Iterfaces
{
    public interface IOrderRepository
    {
        Task<Order> Get(int id);
        Task<IEnumerable<Order>> Get();
        Task<int> Create(Order order);
        Task<Order> Update(Order order);
        Task<int> Delete(int id);
    }
}
