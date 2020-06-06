using System.Collections.Generic;
using System.Linq;
using CustomerOrders;

namespace OrderingSystem.DomainServices
{
    public static class TypeExtensions
    {
        public static IReadOnlyCollection<string> ProductsNames(this IEnumerable<ProductQuantity> products)
        {
            return products.Select(p => p.ProductName).ToList();
        }
    }
}
