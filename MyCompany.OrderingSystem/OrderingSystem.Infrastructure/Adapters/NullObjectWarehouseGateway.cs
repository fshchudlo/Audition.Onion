using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders;
using OrderingSystem.DomainServices.Ports;

namespace OrderingSystem.Infrastructure.Adapters
{
    internal class NullObjectWarehouseGateway: IWarehouseGateway
    {
        public Task<IReadOnlyCollection<StockItem>> GetAvailableStocksAsync(IReadOnlyCollection<string> productsNames)
        {
            IReadOnlyCollection<StockItem> result = productsNames.Select(pn => new StockItem(pn, "Supplier", 0))
                .ToList();
            return Task.FromResult(result);
        }
    }
}
