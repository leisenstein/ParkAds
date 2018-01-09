using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DB.Interfaces
{
    public interface IUnitOfWork<TRepository> : IDisposable where TRepository : class
    {
        TRepository Repository { get; }
        int Complete();
        void Dispose();
    }
}
