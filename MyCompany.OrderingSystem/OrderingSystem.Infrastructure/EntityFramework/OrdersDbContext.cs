using Microsoft.EntityFrameworkCore;

namespace OrderingSystem.Infrastructure.EntityFramework
{
    internal class OrdersDbContext: DbContext
    {
        public OrdersDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
