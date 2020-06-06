using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierOrders.Entities;

namespace OrderingSystem.Infrastructure.EntityFramework.Configurations
{
    public class SupplierOrderConfiguration : IEntityTypeConfiguration<SupplierOrder>
    {
        public void Configure(EntityTypeBuilder<SupplierOrder> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.SupplierName).IsRequired().HasMaxLength(256);

            builder.HasMany(e => e.OrderPositions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}