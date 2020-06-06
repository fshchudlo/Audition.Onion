using System.Threading.Tasks;
using SupplierOrders.Entities;

namespace OrderingSystem.DomainServices.Ports
{
    public interface ISupplierOrderRepository
    {
        Task SaveAsync(SupplierOrder order);
    }
}