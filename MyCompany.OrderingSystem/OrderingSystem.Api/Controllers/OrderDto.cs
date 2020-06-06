using System.Collections.Generic;

namespace OrderingSystem.WebApi.Controllers
{
    public class OrderDto
    {
        public IEnumerable<OrderPositionDto> Positions { get; set; }
    }

    public class OrderPositionDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}