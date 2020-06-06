using CustomerOrders.Entities;

namespace CustomerOrders
{
    public sealed class ProductQuantity
    {
        public ProductQuantity(CustomerOrderPosition orderPosition)
        {
            ProductName = orderPosition.ProductName;
            Quantity = orderPosition.OrderedQuantity;
        }

        public ProductQuantity()
        {
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}