using Microsoft.Data.SqlClient;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;
using System.Runtime.InteropServices.Marshalling;

namespace Store.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> Create(Order order)
        {
            string sql = "INSERT INTO Orders(OrderDate,TotalAmount, UserId) OUTPUT INSERTED.Id VALUES (@OrderDate, @TotalAmount, @UserId)";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue($"@{nameof(order.OrderDate)}", order.OrderDate);
            command.Parameters.AddWithValue($"@{nameof(order.TotalAmount)}", order.TotalAmount);
            command.Parameters.AddWithValue($"@{nameof(order.UserId)}", order.UserId);

            await connection.OpenAsync();

            int orderId = (int)await command.ExecuteScalarAsync();

            foreach (var orderitem in order.OrderItems)
            {
                string sqlOrderItem = "INSERT INTO OrderItems(Id, OrderId, BookId, Price, Quantity) VALUES (@Id, @OrderId, @BookId, @Price, @Quantity)";
                using SqlCommand orderItemCommand = new(sqlOrderItem, connection);

                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.OrderId)}", orderId);
                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.BookId)}", orderitem.BookId);
                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.Price)}", orderitem.Price);
                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.Quantity)}", orderitem.Quantity);

                await orderItemCommand.ExecuteNonQueryAsync();
            }

            return orderId;
        }

        public async Task<int> Delete(int id)
        {
            string sql = "DELETE FROM Orders WHERE Id = @id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            return command.ExecuteNonQuery();
        }

        public async Task<Order> Get(int id)
        {
            string sql = @"SELECT o.Id AS OrderId, o.OrderDate, o.TotalAmount, o.UserId, oi.Id AS OrderItemId, oi.BookId, oi.Quantity, oi.Price
                           FROM Orders o 
                           LEFT JOIN OrderItems oi ON oi.OrderId = o.Id
                           WHERE o.Id = @id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var order = new Order();

            while (await reader.ReadAsync())
            {
                if (order.Id == 0)
                {
                    order.Id = Convert.ToInt32(reader["OrderId"]);
                    order.OrderDate = Convert.ToDateTime(reader[$"{nameof(order.OrderDate)}"]);
                    order.TotalAmount = Convert.ToDecimal(reader[$"{nameof(order.TotalAmount)}"]);
                    order.UserId = Convert.ToInt32(reader[$"{nameof(order.UserId)}"]);
                }

                AddOrderItem(reader, order);
            }

            return order;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            string sql = @"SELECT o.Id AS OrderId, o.OrderDate, o.TotalAmount, o.UserId, 
                                  oi.Id AS OrderItemId, oi.BookId, oi.Quantity, oi.Price
                           FROM Orders o 
                           LEFT JOIN OrderItems oi ON oi.OrderId = o.Id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            var orders = new Dictionary<int, Order>();

            while (await reader.ReadAsync())
            {
                var orderId = Convert.ToInt32(reader["OrderId"]);

                if (!orders.TryGetValue(orderId, out var order))
                {
                    order = new Order
                    {
                        Id = orderId,
                        OrderDate = Convert.ToDateTime(reader[$"{nameof(order.OrderDate)}"]),
                        TotalAmount = Convert.ToDecimal(reader[$"{nameof(order.TotalAmount)}"]),
                        UserId = Convert.ToInt32(reader[$"{nameof(order.UserId)}"]),
                    };

                    orders.Add(orderId, order);
                }

                AddOrderItem(reader, order);
            }

            return orders.Values;
        }

        public async Task<Order> Update(Order order)
        {
            string sql = "UPDATE Orders SET OrderDate = @OrderDate, TotalAmount = @TotalAmount, UserId = @UserId WHERE Id = @Id";

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue($"@{nameof(order.Id)}", order.Id);
            command.Parameters.AddWithValue($"@{nameof(order.OrderDate)}", order.OrderDate);
            command.Parameters.AddWithValue($"@{nameof(order.TotalAmount)}", order.TotalAmount);
            command.Parameters.AddWithValue($"@{nameof(order.UserId)}", order.UserId);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();

            string deleteSql = "DELETE FROM OrderItems WHERE OrderId = @OrderId";

            using SqlCommand deleteCommand = new(deleteSql, connection);

            deleteCommand.Parameters.AddWithValue("@OrderId", order.Id);

            await deleteCommand.ExecuteNonQueryAsync();

            foreach (var orderitem in order.OrderItems)
            {
                string sqlOrderItem = "INSERT INTO OrderItems(Id, OrderId, BookId, Price, Quantity) VALUES (@Id, @OrderId, @BookId, @Price, @Quantity)";
                using SqlCommand orderItemCommand = new(sqlOrderItem, connection);

                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.OrderId)}", order.Id);
                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.BookId)}", orderitem.BookId);
                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.Price)}", orderitem.Price);
                orderItemCommand.Parameters.AddWithValue($"@{nameof(orderitem.Quantity)}", orderitem.Quantity);

                await orderItemCommand.ExecuteNonQueryAsync();
            }

            order = await Get(order.Id);

            return order;
        }

        private void AddOrderItem(SqlDataReader reader, Order order)
        {
            var orderItemId = Convert.ToInt32(reader["OrderItemId"]);

            if (!order.OrderItems.Any(oi => oi.Id == orderItemId))
            {
                var orderItem = new OrderItem
                {
                    Id = orderItemId,
                    Quantity = Convert.ToInt32(reader[$"{nameof(OrderItem.Quantity)}"]),
                    Price = Convert.ToDecimal(reader[$"{nameof(OrderItem.Price)}"]),
                    BookId = Convert.ToInt32(reader[$"{nameof(OrderItem.BookId)}"])
                };

                order.OrderItems.Add(orderItem);
            }
        }
    }
}
