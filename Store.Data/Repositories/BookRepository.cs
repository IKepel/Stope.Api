using Microsoft.Data.SqlClient;
using Store.Data.Constants;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

namespace Store.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(Book book)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(SqlConstants.BookSqlConstants.INSERT_BOOK, connection);

            AddBookParameters(command, book);

            await connection.OpenAsync();

            int bookId = (int)await command.ExecuteScalarAsync();

            await InsertAuthorsAsync(connection, book.Id, book.Authors);

            await InsertCategoriesAsync(connection, book.Id, book.Categories);

            await InsertDetailsAsync(connection, book.Id, book.BookDetails);

            return bookId;
        }

        public async Task<int> Delete(int id)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(SqlConstants.BookSqlConstants.DELETE_BOOK, connection);

            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            return command.ExecuteNonQuery();
        }

        public async Task<Book> Get(int id)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(SqlConstants.BookSqlConstants.GET_BOOK_BY_ID, connection);

            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var book = new Book();

            while (await reader.ReadAsync())
            {
                if (book.Id == 0)
                {
                    book.Id = Convert.ToInt32(reader["Id"]);
                    book.Name = reader[$"{nameof(book.Name)}"].ToString();
                    book.Description = reader[$"{nameof(book.Description)}"].ToString();
                    book.Price = Convert.ToDecimal(reader[$"{nameof(book.Price)}"]);
                    book.PublishedDate = Convert.ToDateTime(reader[$"{nameof(book.PublishedDate)}"]);
                }

                AddAuthors(reader, book);

                AddCategoties(reader, book);

                AddDetails(reader, book);
            }

            return book;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(SqlConstants.BookSqlConstants.GET_ALL_BOOKS, connection);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var bookDictionary = new Dictionary<int, Book>();

            while (await reader.ReadAsync())
            {
                var bookId = Convert.ToInt32(reader["Id"]);

                if (!bookDictionary.TryGetValue(bookId, out var book))
                {
                    book = new Book
                    {
                        Id = bookId,
                        Name = reader[$"{nameof(book.Name)}"].ToString(),
                        Description = reader[$"{nameof(book.Description)}"].ToString(),
                        Price = Convert.ToDecimal(reader[$"{nameof(book.Price)}"]),
                        PublishedDate = Convert.ToDateTime(reader[$"{nameof(book.PublishedDate)}"]),
                    };

                    bookDictionary.Add(bookId, book);
                }

                AddAuthors(reader, book);

                AddCategoties(reader, book);

                AddDetails(reader, book);
            }

            return bookDictionary.Values;
        }

        public async Task<Book> Update(Book book)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(SqlConstants.BookSqlConstants.UPDATE_BOOK, connection);

            command.Parameters.AddWithValue("@Id", book.Id);
            AddBookParameters(command, book);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            using SqlCommand deleteAuthorBookCommand = new(SqlConstants.AuthorBookSqlConstants.DELETE, connection);

            deleteAuthorBookCommand.Parameters.AddWithValue("@BookId", book.Id);

            await deleteAuthorBookCommand.ExecuteNonQueryAsync();

            await InsertAuthorsAsync(connection, book.Id, book.Authors);

            using SqlCommand deleteBookCategoryCommand = new(SqlConstants.BookCategorySqlConstants.DELETE, connection);

            deleteBookCategoryCommand.Parameters.AddWithValue("@BookId", book.Id);

            await deleteBookCategoryCommand.ExecuteNonQueryAsync();

            await InsertCategoriesAsync(connection, book.Id, book.Categories);

            using SqlCommand deleteDatailCommand = new(SqlConstants.BookDetailSqlConstants.DELETE_BOOKDETAIL, connection);

            deleteDatailCommand.Parameters.AddWithValue("@BookId", book.Id);

            await deleteDatailCommand.ExecuteNonQueryAsync();

            await InsertDetailsAsync(connection, book.Id, book.BookDetails);

            book = await Get(book.Id);

            return book;
        }

        private void AddBookParameters(SqlCommand command, Book book)
        {
            command.Parameters.AddWithValue($"@{nameof(book.Name)}", book.Name);
            command.Parameters.AddWithValue($"@{nameof(book.Description)}", book.Description);
            command.Parameters.AddWithValue($"@{nameof(book.Price)}", book.Price);
            command.Parameters.AddWithValue($"@{nameof(book.PublishedDate)}", book.PublishedDate);
        }

        private void AddAuthors(SqlDataReader reader, Book book)
        {
            var authorId = Convert.ToInt32(reader["AuthorId"]);

            if (!book.Authors.Any(a => a.Id == authorId))
            {
                var author = new Author
                {
                    Id = authorId,
                    FirstName = reader[$"{nameof(Author.FirstName)}"].ToString(),
                    LastName = reader[$"{nameof(Author.LastName)}"].ToString()
                };

                book.Authors.Add(author);
            }
        }

        private void AddCategoties(SqlDataReader reader, Book book)
        {
            var categoryId = Convert.ToInt32(reader["CategoryId"]);

            if (!book.Categories.Any(c => c.Id == categoryId))
            {
                var category = new Category
                {
                    Id = categoryId,
                    Name = reader[$"Category{nameof(Category.Name)}"].ToString()
                };

                book.Categories.Add(category);
            }
        }

        private void AddDetails(SqlDataReader reader, Book book)
        {
            var detailId = Convert.ToInt32(reader["DetailId"]);

            if (!book.BookDetails.Any(d => d.Id == detailId))
            {
                var bookDetail = new BookDetail
                {
                    Id = detailId,
                    Language = reader[$"{nameof(BookDetail.Language)}"].ToString(),
                    PageCount = Convert.ToInt32(reader[$"{nameof(BookDetail.PageCount)}"]),
                    Publisher = reader[$"{nameof(BookDetail.Publisher)}"].ToString()
                };

                book.BookDetails.Add(bookDetail);
            }
        }

        private async Task InsertAuthorsAsync(SqlConnection connection, int bookId, List<Author> authors)
        {
            foreach (var author in authors)
            {
                using SqlCommand insertAuthorBookCommand = new(SqlConstants.AuthorBookSqlConstants.INSERT, connection);

                insertAuthorBookCommand.Parameters.AddWithValue("@BookId", bookId);
                insertAuthorBookCommand.Parameters.AddWithValue("@AuthorId", author.Id);

                await insertAuthorBookCommand.ExecuteNonQueryAsync();
            }
        }

        private async Task InsertCategoriesAsync(SqlConnection connection, int bookId, List<Category> categories)
        {
            foreach (var category in categories)
            {
                using SqlCommand insertBookCategoryCommand = new(SqlConstants.BookCategorySqlConstants.INSERT, connection);

                insertBookCategoryCommand.Parameters.AddWithValue("@BookId", bookId);
                insertBookCategoryCommand.Parameters.AddWithValue("@CategoryId", category.Id);

                await insertBookCategoryCommand.ExecuteNonQueryAsync();
            }
        }

        private async Task InsertDetailsAsync(SqlConnection connection, int bookId, List<BookDetail> bookDetails)
        {
            foreach (var detail in bookDetails)
            {
                using SqlCommand insertDetailCommand = new(SqlConstants.BookDetailSqlConstants.INSERT_BOOKDETAIL, connection);

                insertDetailCommand.Parameters.AddWithValue($"@{nameof(detail.BookId)}", bookId);
                insertDetailCommand.Parameters.AddWithValue($"@{nameof(detail.Language)}", detail.Language);
                insertDetailCommand.Parameters.AddWithValue($"@{nameof(detail.PageCount)}", detail.PageCount);
                insertDetailCommand.Parameters.AddWithValue($"@{nameof(detail.Publisher)}", detail.Publisher);

                await insertDetailCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
