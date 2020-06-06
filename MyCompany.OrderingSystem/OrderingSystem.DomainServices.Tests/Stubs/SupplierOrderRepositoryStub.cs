using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.DomainServices.Ports;
using SupplierOrders.Entities;

namespace OrderingSystem.DomainServices.Tests.Stubs
{
    internal class SupplierOrderRepositoryStub : ISupplierOrderRepository
    {
        public List<SupplierOrder> SavedOrders { get; } = new List<SupplierOrder>();

        public Task SaveAsync(SupplierOrder order)
        {
            SavedOrders.Add(order);
            return Task.CompletedTask;
        }
    }
}