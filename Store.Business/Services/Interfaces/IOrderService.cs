using Store.Business.Models.Orders;

namespace Store.Business.Services.Interfaces
{
    public interface IOrderService
    {
        OrderModel Get(int id);
    }
}
