namespace Store.Data.Constants
{
    public sealed class SqlConstants
    {

        public class BookSqlConstants
        {
            public const string GET_BOOK_BY_ID =
                @"SELECT 
			        b.Id, b.Name, b.Description, b.Price, b.PublishedDate,
			        d.Id AS DetailId, d.Language, d.PageCount, d.Publisher, 
			        c.Id AS CategoryId, c.Name AS CategoryName,
			        a.Id AS AuthorId, a.FirstName, a.LastName
                FROM Books b
				LEFT JOIN BookDetails d ON d.BookId = b.Id
				LEFT JOIN BookCategory bc ON bc.BookId = b.Id
				LEFT JOIN Categories c ON c.Id = bc.CategoryId
                LEFT JOIN AuthorBook ab ON ab.BookId = b.Id
                LEFT JOIN Authors a ON a.Id = ab.AuthorId
                WHERE b.Id = @Id";

            public const string GET_ALL_BOOKS =
                @"SELECT 
			        b.Id, b.Name, b.Description, b.Price, b.PublishedDate,
			        d.Id AS DetailId, d.Language, d.PageCount, d.Publisher, 
			        c.Id AS CategoryId, c.Name AS CategoryName,
			        a.Id AS AuthorId, a.FirstName, a.LastName
                FROM Books b
				LEFT JOIN BookDetails d ON d.BookId = b.Id
				LEFT JOIN BookCategory bc ON bc.BookId = b.Id
				LEFT JOIN Categories c ON c.Id = bc.CategoryId
                LEFT JOIN AuthorBook ab ON ab.BookId = b.Id
                LEFT JOIN Authors a ON a.Id = ab.AuthorId";

            public const string DELETE_BOOK = "DELETE FROM Books WHERE Id = @id";

            public const string INSERT_BOOK =
                @"INSERT INTO Books (Name, Description, Price, PublishedDate) 
                OUTPUT INSERTED.Id 
                VALUES (@Name, @Description, @Price, @PublishedDate)";

            public const string UPDATE_BOOK = @"UPDATE Books SET Name = @Name, Description = @Description, 
                                              Price = @Price, PublishedDate = @PublishedDate WHERE Id = @Id";

        }

        public class AuthorBookSqlConstants
        {
            public const string INSERT = @"INSERT INTO AuthorBook (BookId, AuthorId) 
                                                    VALUES (@BookId, @AuthorId)";

            public const string DELETE = "DELETE FROM AuthorBook WHERE BookId = @BookId";
        }

        public class BookCategorySqlConstants
        {
            public const string INSERT = @"INSERT INTO BookCategory (BookId, CategoryId) 
                                                      VALUES (@BookId, @CategoryId)";

            public const string DELETE = "DELETE FROM BookCategory WHERE BookId = @BookId";
        }

        public class BookDetailSqlConstants
        {
            public const string INSERT_BOOKDETAIL = @"INSERT INTO BookDetails (BookId, Language, PageCount, Publisher) 
                                                    VALUES (@BookId, @Language, @PageCount, @Publisher)";

            public const string DELETE_BOOKDETAIL = "DELETE FROM BookDetails WHERE BookId = @BookId";
        }
    }
}
