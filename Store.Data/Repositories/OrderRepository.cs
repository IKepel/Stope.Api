using Microsoft.Data.SqlClient;
using Store.Data.Dtos;
using Store.Data.Entities;
using Store.Data.Repositories.Iterfaces;
using System.Data;
using System.Text;

namespace Store.Data.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<int?> Create(Order order)
        {
            var insertOrderSql = new StringBuilder(@"DECLARE @Id INT 
                            INSERT INTO Orders(OrderDate, TotalAmount, UserId) OUTPUT INSERTED.Id 
                            VALUES (@OrderDate, @TotalAmount, @UserId)
                            SET @Id = SCOPE_IDENTITY()
                            ");

            AddOrderItemInserts(order, insertOrderSql);

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = insertOrderSql.ToString();

                AddParameter(command, nameof(Order.OrderDate), order.OrderDate);
                AddParameter(command, nameof(Order.TotalAmount), order.TotalAmount);
                AddParameter(command, nameof(Order.UserId), order.UserId);

                AddOrderItemParemeters(order, command);

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
            string sql = "DELETE FROM Orders WHERE Id = @Id";

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = sql;

                AddParameter(command, nameof(Order.Id), id);

                return await ExecuteNonQueryAsync(command);
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<Order?> Get(int id)
        {
            string getBookByIdSql = @"SELECT o.Id, o.OrderDate, o.TotalAmount, o.UserId,
	                        u.FirstName, u.LastName, u.Email,
	                        oi.Id AS OrderItemId, oi.BookId, b.Name AS BookName, oi.Quantity, oi.Price
                          FROM Orders o 
                          LEFT JOIN OrderItems oi ON oi.OrderId = o.Id
                          LEFT JOIN Books b ON oi.BookId = b.Id
                          LEFT JOIN Users u ON o.UserId = u.Id
                          WHERE o.Id = @Id";


            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = getBookByIdSql;

                AddParameter(command, nameof(Order.Id), id);

                using var reader = await ExecuteReaderAsync(command);

                if (!((SqlDataReader)reader).HasRows)
                    return null;

                var orderDtos = new List<OrderDto>();

                while (await ((SqlDataReader)reader).ReadAsync())
                {
                    var orderDto = new OrderDto
                    {
                        Id = Convert.ToInt32(reader[nameof(OrderDto.Id)]),
                        OrderDate = Convert.ToDateTime(reader[nameof(OrderDto.OrderDate)]),
                        TotalAmount = Convert.ToDecimal(reader[nameof(OrderDto.TotalAmount)]),
                        UserId = Convert.ToInt32(reader[nameof(OrderDto.UserId)]),
                        FirstName = reader[nameof(OrderDto.FirstName)].ToString()!,
                        LastName = reader[nameof(OrderDto.LastName)].ToString()!,
                        Email = reader[nameof(OrderDto.Email)].ToString()!,
                        OrderItemId = Convert.ToInt32(reader[nameof(OrderDto.OrderItemId)]),
                        BookId = Convert.ToInt32(reader[nameof(OrderDto.BookId)]),
                        BookName = reader[nameof(OrderDto.BookName)].ToString()!,
                        Price = Convert.ToDecimal(reader[nameof(OrderDto.Price)]),
                        Quantity = Convert.ToInt32(reader[nameof(OrderDto.Quantity)])
                    };

                    orderDtos.Add(orderDto);
                }

                var order = new Order
                {
                    Id = orderDtos.First().Id,
                    OrderDate = orderDtos.First().OrderDate,
                    TotalAmount = orderDtos.First().TotalAmount,
                    UserId = orderDtos.First().UserId,
                    User = new User
                    {
                        FirstName = orderDtos.First().FirstName,
                        LastName = orderDtos.First().LastName,
                        Email = orderDtos.First().Email,
                    },
                    OrderItems = orderDtos.GroupBy(oi => oi.OrderItemId)
                    .Select(group => new OrderItem
                    {
                        Id = group.Key,
                        BookId = group.First().BookId,
                        Book = new Book
                        {
                            Name = group.First().BookName
                        },
                        Price = group.First().Price,
                        Quantity = group.First().Quantity
                    }).ToList()
                };

                return order;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<IEnumerable<OrderDto>> Get()
        {
            string getAllBookSql = @"SELECT o.Id, o.OrderDate, o.TotalAmount, o.UserId,
	                        u.FirstName, u.LastName, u.Email,
	                        oi.Id AS OrderItemId, oi.BookId, b.Name AS BookName, oi.Quantity, oi.Price
                          FROM Orders o 
                          LEFT JOIN OrderItems oi ON oi.OrderId = o.Id
                          LEFT JOIN Books b ON oi.BookId = b.Id
                          LEFT JOIN Users u ON o.UserId = u.Id
                          ";

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = getAllBookSql;

                using var reader = await ExecuteReaderAsync(command);

                var orderDtos = new List<OrderDto>();

                while (await ((SqlDataReader)reader).ReadAsync())
                {
                    var orderDto = new OrderDto
                    {
                        Id = Convert.ToInt32(reader[nameof(OrderDto.Id)]),
                        OrderDate = Convert.ToDateTime(reader[nameof(OrderDto.OrderDate)]),
                        TotalAmount = Convert.ToDecimal(reader[nameof(OrderDto.TotalAmount)]),
                        UserId = Convert.ToInt32(reader[nameof(OrderDto.UserId)]),
                        FirstName = reader[nameof(OrderDto.FirstName)].ToString()!,
                        LastName = reader[nameof(OrderDto.LastName)].ToString()!,
                        Email = reader[nameof(OrderDto.Email)].ToString()!,
                        OrderItemId = Convert.ToInt32(reader[nameof(OrderDto.OrderItemId)]),
                        BookId = Convert.ToInt32(reader[nameof(OrderDto.BookId)]),
                        BookName = reader[nameof(OrderDto.BookName)].ToString()!,
                        Price = Convert.ToDecimal(reader[nameof(OrderDto.Price)]),
                        Quantity = Convert.ToInt32(reader[nameof(OrderDto.Quantity)])
                    };

                    orderDtos.Add(orderDto);
                }

                return orderDtos;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<Order?> Update(Order order)
        {
            var updateOrderSql = new StringBuilder(@"UPDATE Orders SET OrderDate = @OrderDate, TotalAmount = @TotalAmount, UserId = @UserId 
                                                    WHERE Id = @Id
                                                    ");

            updateOrderSql.AppendLine("DELETE FROM OrderItems WHERE OrderId = @Id");

            AddOrderItemInserts(order, updateOrderSql);

            try
            {
                await OpenConnectionAsync();

                using var command = CreateCommand();
                command.CommandText = updateOrderSql.ToString();

                AddParameter(command, nameof(Order.Id), order.Id);
                AddParameter(command, nameof(Order.OrderDate), order.OrderDate);
                AddParameter(command, nameof(Order.TotalAmount), order.TotalAmount);
                AddParameter(command, nameof(Order.UserId), order.UserId);

                AddOrderItemParemeters(order, command);

                await ExecuteNonQueryAsync(command);

                var updatedOrder = await Get(order.Id);

                return updatedOrder;
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        private void AddOrderItemInserts(Order order, StringBuilder builder)
        {
            var paramIndex = 0;
            foreach (var item in order.OrderItems)
            {
                builder.AppendLine($@"INSERT INTO OrderItems(OrderId, BookId, Price, Quantity) 
                                        VALUES (@Id, @BookId{paramIndex}, @Price{paramIndex}, @Quantity{paramIndex})");
                paramIndex++;
            }
        }

        private void AddOrderItemParemeters(Order order, IDbCommand command)
        {
            var paramIndex = 0;
            foreach (var item in order.OrderItems)
            {
                AddParameter(command, $"{nameof(OrderItem.BookId)}{paramIndex}", item.BookId);
                AddParameter(command, $"{nameof(OrderItem.Price)}{paramIndex}", item.Price);
                AddParameter(command, $"{nameof(OrderItem.Quantity)}{paramIndex}", item.Quantity);
                paramIndex++;
            }
        }
    }
}
