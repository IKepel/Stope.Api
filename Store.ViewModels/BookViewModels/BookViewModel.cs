namespace Store.ViewModels.ProductViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }

        //public virtual ICollection<Author> Authors { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }

        //public virtual ICollection<BookDetail> BookDetails { get; set; }

        //public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int Status { get; set; }
    }
}
