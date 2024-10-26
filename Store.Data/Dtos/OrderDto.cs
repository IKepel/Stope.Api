namespace Store.Data.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int OrderItemId { get; set; }

        public string BookName { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
