using Store.Data.Dtos;
using Store.Data.Entities;

namespace Store.Data.Repositories.Iterfaces
{
    public interface IOrderRepository
    {
        Task<Order?> Get(int id);
        Task<IEnumerable<OrderDto>> Get();
        Task<int?> Create(Order order);
        Task<Order?> Update(Order order);
        Task<int> Delete(int id);
    }
}
