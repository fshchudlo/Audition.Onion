using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders;

namespace OrderingSystem.DomainServices.Ports
{
    public interface IWarehouseGateway
    {
        Task<IReadOnlyCollection<StockItem>> GetAvailableStocksAsync(IReadOnlyCollection<string> productsNames);
    }
}