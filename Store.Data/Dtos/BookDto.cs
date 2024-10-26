namespace Store.Data.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int DetailId { get; set; }
        public string Language {  get; set; }
        public int PageCount { get; set; }
        public string Publisher {  get; set; }
    }
}
