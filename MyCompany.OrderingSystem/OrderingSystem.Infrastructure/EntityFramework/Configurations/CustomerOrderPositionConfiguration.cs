using CustomerOrders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingSystem.Infrastructure.EntityFramework.Configurations
{
    public class CustomerOrderPositionConfiguration : IEntityTypeConfiguration<CustomerOrderPosition>
    {
        public void Configure(EntityTypeBuilder<CustomerOrderPosition> builder)
        {
            builder.Property<int>("Id").HasColumnType("int").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}