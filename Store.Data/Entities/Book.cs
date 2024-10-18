using Microsoft.Data.SqlClient;

namespace Store.Data.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<BookDetail> BookDetails { get; set; } = new List<BookDetail>();

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
