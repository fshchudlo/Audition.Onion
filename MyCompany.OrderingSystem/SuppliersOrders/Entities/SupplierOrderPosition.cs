namespace SupplierOrders.Entities
{
    public sealed class SupplierOrderPosition
    {
        private SupplierOrderPosition()
        {
            
        }
        public SupplierOrderPosition(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
    }
}