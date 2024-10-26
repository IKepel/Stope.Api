using Microsoft.Data.SqlClient;
using Store.Data.Dtos;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;
using System.Data;
using System.Text;

namespace Store.Data.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<int?> Create(Book book)
        {
            var insertBookSql = new StringBuilder(@"DECLARE @Id INT
            INSERT INTO Books (Name, Description, Price, PublishedDate)
            OUTPUT INSERTED.Id
            VALUES (@Name, @Description, @Price, @PublishedDate)
            SET @Id = SCOPE_IDENTITY()
            ");

            AddAuthorInserts(book, insertBookSql);
            AddCategoryInserts(book, insertBookSql);
            AddBookDetailInserts(book, insertBookSql);

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = insertBookSql.ToString();

                AddParameter(command, nameof(Book.Name), book.Name);
                AddParameter(command, nameof(Book.Description), book.Description);
                AddParameter(command, nameof(Book.Price), book.Price);
                AddParameter(command, nameof(Book.PublishedDate), book.PublishedDate);

                AddAuthorParameters(book, command);
                AddCategoryParameters(book, command);
                AddBookDetailParameters(book, command);

                var result = await ExecuteScalarAsync(command);

                return result is int id ? id : null;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<int> Delete(int id)
        {
            var deleteBookQuery = "DELETE FROM Books WHERE Id = @Id";

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = deleteBookQuery;

                AddParameter(command, nameof(Book.Id), id);

                return await ExecuteNonQueryAsync(command);
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<Book?> Get(int id)
        {
            var getBookByIdSql = @"SELECT 
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

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();

                command.CommandText = getBookByIdSql;

                AddParameter(command, nameof(Book.Id), id);

                using var reader = await ExecuteReaderAsync(command);

                if (!((SqlDataReader)reader).HasRows)
                    return null;

                var bookDtos = new List<BookDto>();

                while (await ((SqlDataReader)reader).ReadAsync())
                {
                    var bookDto = new BookDto
                    {
                        Id = Convert.ToInt32(reader[nameof(BookDto.Id)]),
                        Name = reader[nameof(BookDto.Name)].ToString()!,
                        Description = reader[nameof(BookDto.Description)].ToString()!,
                        Price = Convert.ToDecimal(reader[nameof(BookDto.Price)]),
                        PublishedDate = Convert.ToDateTime(reader[nameof(BookDto.PublishedDate)]),
                        AuthorId = Convert.ToInt32(reader[nameof(BookDto.AuthorId)]),
                        FirstName = reader[nameof(BookDto.FirstName)].ToString()!,
                        LastName = reader[nameof(BookDto.LastName)].ToString()!,
                        CategoryId = Convert.ToInt32(reader[nameof(BookDto.CategoryId)]),
                        CategoryName = reader[nameof(BookDto.CategoryName)].ToString()!,
                        DetailId = Convert.ToInt32(reader[nameof(BookDto.DetailId)]),
                        Language = reader[nameof(BookDto.Language)].ToString()!,
                        PageCount = Convert.ToInt32(reader[nameof(BookDto.PageCount)]),
                        Publisher = reader[nameof(BookDto.Publisher)].ToString()!
                    };

                    bookDtos.Add(bookDto);
                }

                var book = new Book
                {
                    Id = bookDtos.First().Id,
                    Name = bookDtos.First().Name,
                    Description = bookDtos.First().Description,
                    Price = bookDtos.First().Price,
                    PublishedDate = bookDtos.First().PublishedDate,
                    Categories = bookDtos.GroupBy(x => x.CategoryId)
                    .Select(group => new Category
                    {
                        Id = group.Key,
                        Name = group.First().CategoryName
                    }).ToList(),
                    Authors = bookDtos.GroupBy(x => x.AuthorId)
                    .Select(group => new Author
                    {
                        Id = group.Key,
                        FirstName = group.First().FirstName,
                        LastName = group.First().LastName
                    }).ToList(),
                    BookDetails = bookDtos.GroupBy(x => x.DetailId)
                    .Select(group => new BookDetail
                    {
                        Id = group.Key,
                        Language = group.First().Language,
                        PageCount = group.First().PageCount,
                        Publisher = group.First().Publisher
                    }).ToList()
                };

                return book;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<IEnumerable<BookDto>> Get()
        {
            var getAllBooksQuery = @"SELECT 
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

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();

                command.CommandText = getAllBooksQuery;

                using var reader = await ExecuteReaderAsync(command);

                var bookDtos = new List<BookDto>();

                while (await ((SqlDataReader)reader).ReadAsync())
                {
                    var bookDto = new BookDto
                    {
                        Id = Convert.ToInt32(reader[nameof(Book.Id)]),
                        Name = reader[nameof(BookDto.Name)].ToString()!,
                        Description = reader[nameof(BookDto.Description)].ToString()!,
                        Price = Convert.ToDecimal(reader[nameof(BookDto.Price)]),
                        PublishedDate = Convert.ToDateTime(reader[nameof(BookDto.PublishedDate)]),
                        AuthorId = Convert.ToInt32(reader[nameof(BookDto.AuthorId)]),
                        FirstName = reader[nameof(BookDto.FirstName)].ToString()!,
                        LastName = reader[nameof(BookDto.LastName)].ToString()!,
                        CategoryId = Convert.ToInt32(reader[nameof(BookDto.CategoryId)]),
                        CategoryName = reader[nameof(BookDto.CategoryName)].ToString()!,
                        DetailId = Convert.ToInt32(reader[nameof(BookDto.DetailId)]),
                        Language = reader[nameof(BookDto.Language)].ToString()!,
                        PageCount = Convert.ToInt32(reader[nameof(BookDto.PageCount)]),
                        Publisher = reader[nameof(BookDto.Publisher)].ToString()!
                    };

                    bookDtos.Add(bookDto);
                }

                return bookDtos;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<Book?> Update(Book book)
        {
            var updateBookSql = new StringBuilder(@"UPDATE Books SET Name = @Name, Description = @Description, 
                                              Price = @Price, PublishedDate = @PublishedDate WHERE Id = @Id
             ");

            updateBookSql.AppendLine("DELETE FROM AuthorBook WHERE BookId = @Id");
            updateBookSql.AppendLine("DELETE FROM BookCategory WHERE BookId = @Id");
            updateBookSql.AppendLine("DELETE FROM BookDetails WHERE BookId = @Id");

            AddAuthorInserts(book, updateBookSql);
            AddCategoryInserts(book, updateBookSql);
            AddBookDetailInserts(book, updateBookSql);

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = updateBookSql.ToString();

                AddParameter(command, nameof(Book.Id), book.Id);
                AddParameter(command, nameof(Book.Name), book.Name);
                AddParameter(command, nameof(Book.Description), book.Description);
                AddParameter(command, nameof(Book.Price), book.Price);
                AddParameter(command, nameof(Book.PublishedDate), book.PublishedDate);

                AddAuthorParameters(book, command);
                AddCategoryParameters(book, command);
                AddBookDetailParameters(book, command);

                await ExecuteNonQueryAsync(command);

                var updatedBook = await Get(book.Id);

                return updatedBook;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        private void AddAuthorInserts(Book book, StringBuilder sqlBuilder)
        {
            var paramIndex = 0;
            foreach (var author in book.Authors)
            {
                sqlBuilder.AppendLine($@"INSERT INTO AuthorBook (BookId, AuthorId)
                                    VALUES (@Id, @AuthorId{paramIndex++});");
            }
        }

        private void AddCategoryInserts(Book book, StringBuilder sqlBuilder)
        {
            var paramIndex = 0;
            foreach (var category in book.Categories)
            {
                sqlBuilder.AppendLine($@" INSERT INTO BookCategory (BookId, CategoryId)
                                                VALUES (@Id, @CategoryId{paramIndex++});");
            }
        }

        private void AddBookDetailInserts(Book book, StringBuilder sqlBuilder)
        {
            var paramIndex = 0;
            foreach (var bookDetail in book.BookDetails)
            {
                sqlBuilder.AppendLine($@"INSERT INTO BookDetails (BookId, Language, PageCount, Publisher)
                    VALUES (@Id, @Language{paramIndex}, @PageCount{paramIndex}, @Publisher{paramIndex});");
                paramIndex++;
            }
        }

        private void AddAuthorParameters(Book book, IDbCommand command)
        {
            var paramIndex = 0;
            foreach (var author in book.Authors)
            {
                AddParameter(command, $"@{nameof(BookDto.AuthorId)}{paramIndex++}", author.Id);
            }
        }

        private void AddCategoryParameters(Book book, IDbCommand command)
        {
            var paramIndex = 0;
            foreach (var category in book.Categories)
            {
                AddParameter(command, $"@{nameof(BookDto.CategoryId)}{paramIndex++}", category.Id);
            }
        }

        private void AddBookDetailParameters(Book book, IDbCommand command)
        {
            var paramIndex = 0;
            foreach (var bookDetail in book.BookDetails)
            {
                AddParameter(command, $"@{nameof(BookDto.Language)}{paramIndex}", bookDetail.Language);
                AddParameter(command, $"@{nameof(BookDto.PageCount)}{paramIndex}", bookDetail.PageCount);
                AddParameter(command, $"@{nameof(BookDto.Publisher)}{paramIndex}", bookDetail.Publisher);
                paramIndex++;
            }
        }
    }
}
