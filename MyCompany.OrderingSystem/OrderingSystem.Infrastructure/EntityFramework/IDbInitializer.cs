using System;

namespace OrderingSystem.Infrastructure.EntityFramework
{
    public interface IDbInitializer : IDisposable
    {
        void Initialize();
    }
}
