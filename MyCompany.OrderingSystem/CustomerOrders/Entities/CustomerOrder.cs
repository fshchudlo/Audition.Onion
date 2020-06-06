using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerOrders.Entities
{
    public sealed class CustomerOrder
    {
        private CustomerOrder()
        {
        }
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }
        public IReadOnlyCollection<CustomerOrderPosition> OrderPositions { get; private set; }

        public bool RequiresOrdering => PositionsToOrder.Any();
        public IReadOnlyCollection<ProductQuantity> PositionsToOrder => OrderPositions
            .Where(op => op.OrderedQuantity > 0)
            .Select(op => new ProductQuantity(op))
            .ToList();

        public static CustomerOrder PlaceAnOrder(SubmitOrderCommand command, IReadOnlyCollection<StockItem> availableStocks)
        {
            var order = new CustomerOrder
            {
                Id = Guid.NewGuid(),
                CustomerName = command.CustomerName,
                CustomerEmail = command.CustomerEmail,
                OrderPositions = command.Positions.MatchWith(availableStocks)
                    .Select(ra => CustomerOrderPosition.Create(ra.Required, ra.Available)).ToList()
            };
            return order;
        }
    }
}