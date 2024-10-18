namespace Store.Data.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
