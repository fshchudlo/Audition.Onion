using OrderingSystem.DomainServices.Ports;

namespace OrderingSystem.Infrastructure.Adapters
{
    internal class NullObjectNotificationService : INotificationService
    {
        public void OrderCreated(CustomerOrderCreatedEvent @event)
        {
            
        }
    }
}