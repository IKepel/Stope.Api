namespace Store.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        //public virtual User User { get; set; }

        //public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int Status { get; set; }
    }
}
