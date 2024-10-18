namespace Store.Data.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
