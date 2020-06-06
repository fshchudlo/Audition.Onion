using System.Collections.Generic;
using System.Linq;

namespace CustomerOrders
{
    public static class TypeExtensions
    {
        public static List<(ProductQuantity Required, StockItem Available)> MatchWith(this IEnumerable<ProductQuantity> requiredPositions, IEnumerable<StockItem> availableStocks)
        {
            return requiredPositions.Select(rp => (rp, availableStocks.Single(s => s.ProductName == rp.ProductName)))
                .ToList();
        }

        public static IReadOnlyDictionary<string, int> ToOrderPositions(this IEnumerable<ProductQuantity> products)
        {
            return products.ToDictionary(p => p.ProductName, p => p.Quantity);
        }
    }
}
