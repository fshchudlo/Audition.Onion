using System;

namespace CustomerOrders.Entities
{
    public sealed class CustomerOrderPosition
    {
        private CustomerOrderPosition()
        {

        }
        private CustomerOrderPosition(string productName, decimal pricePerUnit)
        {
            ProductName = productName;
            PricePerUnit = pricePerUnit;
        }
        public string ProductName { get; private set; }
        public decimal PricePerUnit { get; private set; }
        public int ReservedQuantity { get; private set; }
        public int OrderedQuantity { get; private set; }

        public static CustomerOrderPosition Create(ProductQuantity required, StockItem available)
        {
            var position = new CustomerOrderPosition(required.ProductName, available.PricePerUnit);
            if (available.IsEmpty)
            {
                return position.FullOrdering(required.Quantity);
            }

            if (available.HasLiquidityControl)
            {
                return position.MeasurableReservation(required, available);
            }
            return position.ByThePieceReservation(required, available);
        }

        private CustomerOrderPosition FullOrdering(int quantity)
        {
            OrderedQuantity = quantity;
            ReservedQuantity = 0;
            return this;
        }

        private CustomerOrderPosition MeasurableReservation(ProductQuantity required, StockItem available)
        {
            if (available.Quantity - required.Quantity > available.IlliquidQuantityLimit)
            {
                return ByThePieceReservation(required, available);
            }

            if (required.Quantity > available.Quantity)
            {
                return FullOrdering(required.Quantity);
            }

            if (required.Quantity * 1.05 > available.Quantity)
            {
                OrderedQuantity = 0;
                ReservedQuantity = available.Quantity;
                return this;
            }
            return FullOrdering(required.Quantity);
        }

        private CustomerOrderPosition ByThePieceReservation(ProductQuantity required, StockItem available)
        {
            OrderedQuantity = available.Quantity >= required.Quantity ? 0 : required.Quantity - available.Quantity;
            ReservedQuantity = Math.Min(available.Quantity, required.Quantity);
            return this;
        }
    }
}
