﻿using Store.Business.Models.BookDetails;

namespace Store.Business.Models.Books
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }

        public List<int> AuthorIds { get; set; }

        public List<int> CategoryIds { get; set; }

        public List<BookDetailModel> BookDetails { get; set; }
    }
}
