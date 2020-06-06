namespace CustomerOrders
{
    public class StockItem
    {
        public StockItem(string productName, string supplierName, int quantity)
        {
            ProductName = productName;
            SupplierName = supplierName;
            Quantity = quantity;
        }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
        public int? IlliquidQuantityLimit { get; set; }
        public decimal PricePerUnit { get; set; }
        public bool HasLiquidityControl => IlliquidQuantityLimit.HasValue;
        public bool IsEmpty => Quantity == 0;
    }
}
