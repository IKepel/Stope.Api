using Microsoft.Data.SqlClient;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

namespace Store.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;";

        public async Task<int> Create(Book book)
        {
            string sql = "INSERT INTO Books(Name,Description, Price, PublishedDate) VALUES (@Name, @Description, @Price, @PublishedDate)";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Name", book.Name);
            command.Parameters.AddWithValue("@Description", book.Description);
            command.Parameters.AddWithValue("@Price", book.Price);
            command.Parameters.AddWithValue("@PublishedDate", book.PublishedDate);

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> Delete(int id)
        {
            string sql = "Delete from Books where Id = @id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            return command.ExecuteNonQuery();
        }

        public async Task<Book> Get(int id)
        {
            string sql = "SELECT * FROM Books WHERE Id = @id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var book = new Book
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    PublishedDate = Convert.ToDateTime(reader["PublishedDate"])
                };

                return book;
            }

            return null;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            string sql = "SELECT * FROM Books";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var bookList = new List<Book>();

            while(await reader.ReadAsync())
            {
                var book = new Book
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    PublishedDate = Convert.ToDateTime(reader["PublishedDate"])
                };

                bookList.Add(book);
            }

            return bookList;
        }

        public async Task<Book> Update(Book book)
        {
            string sql = @"UPDATE Books SET Name = @Name, Description = @Description, 
                Price = @Price, PublishedDate = @PublishedDate WHERE Id = @Id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", book.Id);
            command.Parameters.AddWithValue("@Name", book.Name);
            command.Parameters.AddWithValue("@Description", book.Description);
            command.Parameters.AddWithValue("@Price", book.Price);
            command.Parameters.AddWithValue("@PublishedDate", book.PublishedDate);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return book;
        }
    }
}
