namespace OrderingSystem.DomainServices.Ports
{
    public interface INotificationService
    {
        void OrderCreated(CustomerOrderCreatedEvent @event);
    }
}