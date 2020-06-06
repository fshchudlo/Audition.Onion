using System.Threading.Tasks;
using CustomerOrders.Entities;

namespace OrderingSystem.DomainServices.Ports
{
    public interface ICustomerOrderRepository
    {
        Task SaveAsync(CustomerOrder order);
    }
}