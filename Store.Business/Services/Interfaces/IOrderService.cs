using Store.Business.Models.Orders;

namespace Store.Business.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel?> Get(int id);

        Task<IEnumerable<OrderModel>> Get();

        Task<int?> Create(OrderModel orderModel);

        Task<OrderModel?> Update(OrderModel orderModel);

        Task<int> Delete(int id);
    }
}
