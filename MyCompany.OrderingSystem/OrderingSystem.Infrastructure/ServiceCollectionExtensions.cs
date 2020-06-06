using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingSystem.DomainServices;
using OrderingSystem.DomainServices.Ports;
using OrderingSystem.Infrastructure.Adapters;
using OrderingSystem.Infrastructure.EntityFramework;

namespace OrderingSystem.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHandlers(this IServiceCollection services)
        {
            services.AddScoped<SubmitOnlineOrderHandler>();
            return services;
        }
        
        public static IServiceCollection ConfigureExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IWarehouseGateway, NullObjectWarehouseGateway>();
            services.AddScoped<INotificationService, NullObjectNotificationService>();
            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrdersDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("OrdersDatabase"), serverOptions => serverOptions.CommandTimeout(120));
            });
            services.AddScoped<ICustomerOrderRepository>(isp=>new DbContextCustomerOrderRepository(isp.GetRequiredService<OrdersDbContext>()));
            services.AddScoped<ISupplierOrderRepository>(isp => new DbContextSupplierOrderRepository(isp.GetRequiredService<OrdersDbContext>()));
            services.AddScoped<IDbInitializer>(isp => new SqlDbInitializer(isp.GetRequiredService<OrdersDbContext>()));
            return services;
        }
    }
}
