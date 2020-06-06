using System.Threading.Tasks;
using CustomerOrders.Entities;
using OrderingSystem.DomainServices.Ports;
using OrderingSystem.Infrastructure.EntityFramework;

namespace OrderingSystem.Infrastructure.Adapters
{
    public class DbContextCustomerOrderRepository: ICustomerOrderRepository
    {
        private readonly OrdersDbContext _dbContext;

        internal DbContextCustomerOrderRepository(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveAsync(CustomerOrder order)
        {
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
