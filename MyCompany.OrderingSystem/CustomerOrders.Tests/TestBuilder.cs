using System.Collections.Generic;

namespace CustomerOrders.Tests
{
    public static class TestBuilder
    {
        public static ProductStub ElectricGenerator => new ProductStub("Electric generator");
        public static ProductStub Cable => new ProductStub("Cable");

        public static ProductQuantity Required(this ProductStub product, int quantity)
        {
            return new ProductQuantity
            {
                ProductName = product.ProductName,
                Quantity = quantity,
            };
        }
        public static StockItem AvailableStock(this ProductStub product, int quantity)
        {
            return new StockItem(product.ProductName, "Test supplier", quantity);
        }
        public static StockItem WithIlliquidQuantityLimit(this StockItem stockItem, int quantity)
        {
            stockItem.IlliquidQuantityLimit = quantity;
            return stockItem;
        }
        public static StockItem EmptyStock(this ProductStub product)
        {
            return new StockItem(product.ProductName, "Test supplier", 0);
        }
        public class ProductStub
        {
            public ProductStub(string productName)
            {
                ProductName = productName;
            }
            public string ProductName { get; }
        }

        public static SubmitOrderCommand GetSubmitCustomerOrderCommand(params ProductQuantity[] positions)
        {
            return new SubmitOrderCommand
            {
                CustomerName = "John",
                CustomerEmail = "jhon@mail.com",
                Positions = new List<ProductQuantity>(positions)
            };
        }
    }
}