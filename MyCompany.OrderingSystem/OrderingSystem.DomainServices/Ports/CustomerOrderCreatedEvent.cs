using CustomerOrders.Entities;

namespace OrderingSystem.DomainServices.Ports
{
    public class CustomerOrderCreatedEvent
    {
        public string CustomerName { get; }
        public string CustomerEmail { get; }
        public CustomerOrderCreatedEvent(CustomerOrder order)
        {
            CustomerName = order.CustomerName;
            CustomerEmail = order.CustomerEmail;
        }
    }
}