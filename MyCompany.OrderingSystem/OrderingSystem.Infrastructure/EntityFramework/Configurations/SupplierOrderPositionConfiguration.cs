using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierOrders.Entities;

namespace OrderingSystem.Infrastructure.EntityFramework.Configurations
{
    public class SupplierOrderPositionConfiguration : IEntityTypeConfiguration<SupplierOrderPosition>
    {
        public void Configure(EntityTypeBuilder<SupplierOrderPosition> builder)
        {
            builder.Property<int>("Id").HasColumnType("int").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}