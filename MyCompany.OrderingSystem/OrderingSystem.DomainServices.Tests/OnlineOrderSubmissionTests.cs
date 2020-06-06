using System.Threading.Tasks;
using CustomerOrders.Tests;
using FluentAssertions;
using OrderingSystem.DomainServices.Tests.Stubs;
using Xunit;

namespace OrderingSystem.DomainServices.Tests
{
    public class OnlineOrderSubmissionTests
    {
        private readonly CustomerOrderRepositoryStub _customerOrderRepository;
        private readonly SupplierOrderRepositoryStub _supplierOrderRepository;
        private readonly NotificationServiceStub _notificationService;
        private readonly WarehouseGatewayStub _warehouseGateway;
        private readonly SubmitOnlineOrderHandler _sut;

        public OnlineOrderSubmissionTests()
        {
            _customerOrderRepository = new CustomerOrderRepositoryStub();
            _supplierOrderRepository = new SupplierOrderRepositoryStub();
            _notificationService = new NotificationServiceStub();
            _warehouseGateway = WarehouseGatewayStub.Empty();
            _sut = new SubmitOnlineOrderHandler(_warehouseGateway, _notificationService, _customerOrderRepository, _supplierOrderRepository);
        }

        [Fact]
        public async Task Should_save_customer_order()
        {
            // setup 
            var command = TestBuilder
                .GetSubmitCustomerOrderCommand(TestBuilder.ElectricGenerator.Required(1));

            // act
            await _sut.HandleAsync(command);

            // assert
            _customerOrderRepository.SavedOrders.Should()
                .ContainSingle(o => o.CustomerEmail == command.CustomerEmail);
        }

        [Fact]
        public async Task Should_send_notification_to_the_customer()
        {
            // setup 
            var command = TestBuilder
                .GetSubmitCustomerOrderCommand(TestBuilder.ElectricGenerator.Required(1));

            // act
            await _sut.HandleAsync(command);

            // assert
            _notificationService.OrderCreatedEvents.Should()
                .ContainSingle(o => o.CustomerEmail == command.CustomerEmail);
        }

        [Fact]
        public async Task Should_not_create_supplier_order_if_stock_quantity_is_enough()
        {
            // setup 
            _warehouseGateway.IncreaseStocks(TestBuilder.ElectricGenerator.AvailableStock(10));
            var command = TestBuilder
                .GetSubmitCustomerOrderCommand(TestBuilder.ElectricGenerator.Required(1));

            // act
            await _sut.HandleAsync(command);

            // assert
            _supplierOrderRepository.SavedOrders.Should()
                .BeEmpty();
        }

        [Fact]
        public async Task Should_create_supplier_order_if_stock_quantity_in_not_enough()
        {
            // setup 
            var command = TestBuilder
                .GetSubmitCustomerOrderCommand(TestBuilder.ElectricGenerator.Required(1));

            // act
            await _sut.HandleAsync(command);

            // assert
            _supplierOrderRepository.SavedOrders.Should()
                .ContainSingle(o => o.SupplierName == TestBuilder.ElectricGenerator.EmptyStock().SupplierName);
        }
    }
}
