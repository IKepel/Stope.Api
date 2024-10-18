using Microsoft.Extensions.DependencyInjection;
using Store.Data.Repositories.Iterfaces;
using Store.Data.Repositories;

namespace Store.Data
{
    public static class Configuration
    {
        public static void Configure(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddTransient<IOrderRepository>(provider => new OrderRepository(connectionString))
                             .AddTransient<IBookRepository>(provider => new BookRepository(connectionString));
        }
    }
}
