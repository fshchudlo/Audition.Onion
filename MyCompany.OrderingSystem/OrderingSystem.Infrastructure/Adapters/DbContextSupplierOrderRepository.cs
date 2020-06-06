using System.Threading.Tasks;
using OrderingSystem.DomainServices.Ports;
using OrderingSystem.Infrastructure.EntityFramework;
using SupplierOrders.Entities;

namespace OrderingSystem.Infrastructure.Adapters
{
    public class DbContextSupplierOrderRepository: ISupplierOrderRepository
    {
        private readonly OrdersDbContext _dbContext;

        internal DbContextSupplierOrderRepository(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveAsync(SupplierOrder order)
        {
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}