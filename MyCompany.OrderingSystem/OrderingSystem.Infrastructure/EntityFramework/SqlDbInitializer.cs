namespace OrderingSystem.Infrastructure.EntityFramework
{
    public sealed class SqlDbInitializer : IDbInitializer
    {
        private readonly OrdersDbContext _context;

        internal SqlDbInitializer(OrdersDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                _context.Database.EnsureCreated();
            }
            finally
            {
                _context?.Dispose();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}