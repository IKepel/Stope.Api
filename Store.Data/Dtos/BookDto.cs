namespace Store.Data.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<BookDetailDto> BookDetails { get; set; } = new List<BookDetailDto>();
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
