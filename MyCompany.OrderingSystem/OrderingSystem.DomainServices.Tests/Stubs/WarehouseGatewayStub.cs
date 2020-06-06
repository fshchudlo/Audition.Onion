using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders;
using CustomerOrders.Tests;
using OrderingSystem.DomainServices.Ports;

namespace OrderingSystem.DomainServices.Tests.Stubs
{
    public class WarehouseGatewayStub : IWarehouseGateway
    {
        private readonly IReadOnlyCollection<StockItem> _stocksToReturn;

        private WarehouseGatewayStub(IReadOnlyCollection<StockItem> availableStocks)
        {
            _stocksToReturn = availableStocks;
        }

        public Task<IReadOnlyCollection<StockItem>> GetAvailableStocksAsync(IReadOnlyCollection<string> productsNames)
        {
            return Task.FromResult(_stocksToReturn);
        }

        public static WarehouseGatewayStub Empty()
        {
            return new WarehouseGatewayStub(new List<StockItem>
            {
                TestBuilder.Cable.EmptyStock(),
                TestBuilder.ElectricGenerator.EmptyStock()
            });
        }

        public void IncreaseStocks(StockItem item)
        {
            _stocksToReturn.Single(i => i.ProductName == item.ProductName).Quantity += item.Quantity;
        }
    }
}