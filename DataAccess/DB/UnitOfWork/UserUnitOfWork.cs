using DataAccess.Interfaces;
using DataAccess.DB.Contexts;
using DataAccess.DB.Repository;

namespace DataAccess.DB.UnitOfWork
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly UserContext context;

        public UserUnitOfWork(UserContext context)
        {
            this.context = context;
            Users = new UserRepository(this.context);
        }

        public IUserRepository Users { get; private set; }

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
