using CustomerOrders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingSystem.Infrastructure.EntityFramework.Configurations
{
    public class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
    {
        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {
            builder.Ignore(e => e.RequiresOrdering);
            builder.Ignore(e => e.PositionsToOrder);
            
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CustomerName).IsRequired().HasMaxLength(256);
            builder.Property(e => e.CustomerEmail).IsRequired().HasMaxLength(256);

            builder.HasMany(e => e.OrderPositions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
