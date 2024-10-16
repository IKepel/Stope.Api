using Store.Business.Models.Orders;
using Store.Data.Requests;

namespace Store.Business.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> Get(int id);

        Task<IEnumerable<OrderModel>> Get();

        Task<int> Create(UpsertOrderRequestModel orderModel);

        Task<OrderModel> Update(UpsertOrderRequestModel orderModel);

        Task<int> Delete(int id);
    }
}
