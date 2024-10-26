using Store.Business.Models.OrderItems;
using Store.Business.Models.Users;

namespace Store.Business.Models.Orders
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }
    }
}
