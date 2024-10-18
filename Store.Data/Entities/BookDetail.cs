namespace Store.Data.Entities
{
    public class BookDetail : BaseEntity
    {
        public int BookId { get; set; }

        public Book Book { get; set; }

        public int PageCount { get; set; }

        public string Language { get; set; }

        public string Publisher { get; set; }
    }
}
