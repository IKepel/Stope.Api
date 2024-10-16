using Microsoft.Data.SqlClient;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;

namespace Store.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;";

        public async Task<int> Create(Order order)
        {
            string sql = "INSERT INTO Orders(OrderDate,TotalAmount, UserId) VALUES (@OrderDate, @TotalAmount, @UserId)";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@UserId", order.UserId);

            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> Delete(int id)
        {
            string sql = "Delete from Orders where Id = @id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            return command.ExecuteNonQuery();
        }

        public async Task<Order> Get(int id)
        {
            string sql = "SELECT * FROM Orders WHERE Id = @id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var order = new Order
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    UserId = Convert.ToInt32(reader["UserId"])
                };

                return order;
            }

            return null;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            string sql = "SELECT * FROM Orders";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var orderList = new List<Order>();

            while (await reader.ReadAsync())
            {
                var order = new Order
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    UserId = Convert.ToInt32(reader["UserId"])
                };

                orderList.Add(order);
            }

            return orderList;
        }

        public async Task<Order> Update(Order order)
        {
            string sql = "UPDATE Orders SET OrderDate = @OrderDate, TotalAmount = @TotalAmount, UserId = @UserId WHERE Id = @Id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@UserId", order.UserId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return order;
        }
    }
}
