using Microsoft.Extensions.DependencyInjection;
using Store.Data.Repositories.Iterfaces;
using Store.Data.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Store.Data
{
    public static class Configuration
    {
        public static void Configure(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));

            serviceCollection.AddTransient<IOrderRepository, OrderRepository>()
                             .AddTransient<IBookRepository, BookRepository>();
        }
    }
}
