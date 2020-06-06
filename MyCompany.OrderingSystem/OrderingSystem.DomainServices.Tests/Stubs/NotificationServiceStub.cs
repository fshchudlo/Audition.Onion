using System.Collections.Generic;
using OrderingSystem.DomainServices.Ports;

namespace OrderingSystem.DomainServices.Tests.Stubs
{
    public class NotificationServiceStub: INotificationService
    {
        public ICollection<CustomerOrderCreatedEvent> OrderCreatedEvents { get; } = new List<CustomerOrderCreatedEvent>();

        public void OrderCreated(CustomerOrderCreatedEvent @event)
        {
            OrderCreatedEvents.Add(@event);
        }
    }
}
