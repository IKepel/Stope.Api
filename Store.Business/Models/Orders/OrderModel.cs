using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Store.Business.Models.OrderItems;

namespace Store.Business.Models.Orders
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }
    }
}
