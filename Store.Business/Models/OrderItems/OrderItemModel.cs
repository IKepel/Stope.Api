using Store.Business.Models.Books;

namespace Store.Business.Models.OrderItems
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int BookId { get; set; }

        public BookModel Book { get; set; }
    }
}
