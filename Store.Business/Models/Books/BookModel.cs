using Store.Business.Models.Authors;
using Store.Business.Models.BookDetails;
using Store.Business.Models.Categories;

namespace Store.Business.Models.Books
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }

        public List<AuthorModel> Authors { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<BookDetailModel> BookDetails { get; set; }
    }
}
