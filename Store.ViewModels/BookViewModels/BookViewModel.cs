using Store.ViewModels.AuthorViewModels;
using Store.ViewModels.BookDetailViewModels;
using Store.ViewModels.CategoryViewModels;
using Store.ViewModels.OrderItemViewModels;

namespace Store.ViewModels.ProductViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<BookDetailViewModel> BookDetails { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        public int Status { get; set; }
    }
}
