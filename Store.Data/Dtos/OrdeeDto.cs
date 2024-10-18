namespace Store.Data.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
