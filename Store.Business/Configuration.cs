using Microsoft.Extensions.DependencyInjection;
using Store.Business.Services;
using Store.Business.Services.Interfaces;

namespace Store.Business
{
    public static class Configuration
    {
        public static void Configure(this IServiceCollection serviceCollection, string connectionString)
        {
            Data.Configuration.Configure(serviceCollection, connectionString);

            serviceCollection.AddTransient<IOrderService, OrderService>()
                             .AddTransient<IBookService, BookService>();
        }
    }
}
