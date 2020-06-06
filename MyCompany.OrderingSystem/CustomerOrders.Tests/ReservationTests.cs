using CustomerOrders.Entities;
using FluentAssertions;
using Xunit;

namespace CustomerOrders.Tests
{
    public class ReservationTests
    {
        [Fact]
        public void It_should_order_position_if_stock_is_empty()
        {
            var required = TestBuilder.ElectricGenerator.Required(10);
            var available = TestBuilder.ElectricGenerator.EmptyStock();

            var position = CustomerOrderPosition.Create(required, available);

            position.ReservedQuantity.Should().Be(0);
            position.OrderedQuantity.Should().Be(required.Quantity);
        }

        [Fact]
        public void It_should_reserve_available_stock_if_quantity_is_enough()
        {
            var required = TestBuilder.ElectricGenerator.Required(10);
            var available = TestBuilder.ElectricGenerator.AvailableStock(3);

            var position = CustomerOrderPosition.Create(required, available);

            position.ReservedQuantity.Should().Be(3);
            position.OrderedQuantity.Should().Be(7);
        }

        [Fact]
        public void It_should_reserve_available_stock_of_measurable_position_if_quantity_is_enough()
        {
            var required = TestBuilder.Cable.Required(800);
            var available = TestBuilder.Cable.AvailableStock(1000)
                .WithIlliquidQuantityLimit(100);

            var position = CustomerOrderPosition.Create(required, available);

            position.ReservedQuantity.Should().Be(800);
            position.OrderedQuantity.Should().Be(0);
        }

        [Fact]
        public void It_should_reserve_whole_quantity_of_measurable_position_if_difference_is_less_than_5_percents()
        {
            var required = TestBuilder.Cable.Required(970);
            var available = TestBuilder.Cable.AvailableStock(1000)
                .WithIlliquidQuantityLimit(100);

            var position = CustomerOrderPosition.Create(required, available);

            position.ReservedQuantity.Should().Be(1000);
            position.OrderedQuantity.Should().Be(0);
        }

        [Fact]
        public void It_should_order_whole_quantity_of_illiquid_if_difference_is_bigger_than_5_percents()
        {
            var required = TestBuilder.Cable.Required(920);
            var available = TestBuilder.Cable.AvailableStock(1000)
                .WithIlliquidQuantityLimit(100);

            var position = CustomerOrderPosition.Create(required, available);

            position.ReservedQuantity.Should().Be(0);
            position.OrderedQuantity.Should().Be(920);
        }

        [Fact]
        public void It_should_order_whole_quantity_of_measurable_position_if_required_is_bigger_than_available()
        {
            var required = TestBuilder.Cable.Required(1100);
            var available = TestBuilder.Cable.AvailableStock(1000)
                .WithIlliquidQuantityLimit(100);

            var position = CustomerOrderPosition.Create(required, available);

            position.ReservedQuantity.Should().Be(0);
            position.OrderedQuantity.Should().Be(1100);
        }
    }
}
