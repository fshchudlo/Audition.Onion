using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.Entities;
using OrderingSystem.DomainServices.Ports;

namespace OrderingSystem.DomainServices.Tests.Stubs
{
    internal class CustomerOrderRepositoryStub : ICustomerOrderRepository
    {
        public List<CustomerOrder> SavedOrders { get; } = new List<CustomerOrder>();

        public Task SaveAsync(CustomerOrder order)
        {
            SavedOrders.Add(order);
            return Task.CompletedTask;
        }
    }
}
