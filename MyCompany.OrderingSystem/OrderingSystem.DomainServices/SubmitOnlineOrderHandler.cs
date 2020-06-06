using System.Linq;
using System.Threading.Tasks;
using CustomerOrders;
using CustomerOrders.Entities;
using OrderingSystem.DomainServices.Ports;
using SupplierOrders.Entities;

namespace OrderingSystem.DomainServices
{
    public class SubmitOnlineOrderHandler
    {
        private readonly IWarehouseGateway _warehouseGateway;
        private readonly INotificationService _notificationService;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly ISupplierOrderRepository _supplierOrderRepository;
        public SubmitOnlineOrderHandler(IWarehouseGateway warehouseGateway, INotificationService notificationService, ICustomerOrderRepository customerOrderRepository, ISupplierOrderRepository supplierOrderRepository)
        {
            _warehouseGateway = warehouseGateway;
            _notificationService = notificationService;
            _customerOrderRepository = customerOrderRepository;
            _supplierOrderRepository = supplierOrderRepository;
        }

        public async Task HandleAsync(SubmitOrderCommand command)
        {
            var availableStocks = await _warehouseGateway.GetAvailableStocksAsync(command.Positions.ProductsNames());

            var customersOrder = CustomerOrder.PlaceAnOrder(command, availableStocks);

            if (customersOrder.RequiresOrdering)
            {
                var supplierOrders = customersOrder.PositionsToOrder
                    .MatchWith(availableStocks).GroupBy(p => p.Available.SupplierName)
                    .Select(supplierPositions =>
                    {
                        var positionsToOrder = supplierPositions.Select(p => p.Required).ToOrderPositions();
                        return SupplierOrder.PlaceOrder(supplierPositions.Key, positionsToOrder);
                    });

                foreach (var supplierOrder in supplierOrders)
                {
                    await _supplierOrderRepository.SaveAsync(supplierOrder);
                }
            }

            await _customerOrderRepository.SaveAsync(customersOrder);
            _notificationService.OrderCreated(new CustomerOrderCreatedEvent(customersOrder));
        }
    }
}