using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUserUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}
