using Store.ViewModels.ProductViewModels;

namespace Store.ViewModels.OrderItemViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BookId { get; set; }

        public BookViewModel Book { get; set; }
    }
}
