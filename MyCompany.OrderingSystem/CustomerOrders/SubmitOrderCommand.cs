using System.Collections.Generic;

namespace CustomerOrders
{
    public sealed class SubmitOrderCommand
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public IReadOnlyCollection<ProductQuantity> Positions { get; set; }
    }
}