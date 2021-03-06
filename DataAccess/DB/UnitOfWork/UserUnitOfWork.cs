﻿using DataAccess.DB.Contexts;
using DataAccess.DB.Repository;
using DataAccess.DB.Interfaces;

namespace DataAccess.DB.UnitOfWork
{
    public class UserUnitOfWork : IUnitOfWork<IUserRepository>
    {
        private readonly UserContext context;

        public UserUnitOfWork(UserContext context)
        {
            this.context = context;
            Repository = new UserRepository(this.context);
        }

        public IUserRepository Repository { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
