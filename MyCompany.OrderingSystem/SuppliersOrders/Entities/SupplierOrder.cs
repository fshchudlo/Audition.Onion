using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplierOrders.Entities
{
    public sealed class SupplierOrder
    {
        public Guid Id { get; private set; }
        public string SupplierName { get; private set; }
        public IReadOnlyCollection<SupplierOrderPosition> OrderPositions { get; private set; }

        public static SupplierOrder PlaceOrder(string supplierName, IReadOnlyDictionary<string, int> positions)
        {
            return new SupplierOrder
            {
                Id = Guid.NewGuid(),
                SupplierName = supplierName,
                OrderPositions = positions.Select(p => new SupplierOrderPosition(p.Key, p.Value)).ToList()
            };
        }
    }
}