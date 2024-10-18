using Store.Business.Models.Orders;
using Store.Data.Dtos;

namespace Store.Business.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> Get(int id);

        Task<IEnumerable<OrderDto>> Get();

        Task<int> Create(OrderModel orderModel);

        Task<OrderDto> Update(OrderModel orderModel);

        Task<int> Delete(int id);
    }
}
