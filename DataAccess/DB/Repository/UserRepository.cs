using DataAccess.Interfaces;
using DataAccess.DB.Contexts;
using Domain;

namespace DataAccess.DB.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    { 
        public UserRepository(UserContext context) : base(context)
        {
        }

        public UserContext UserContext
        {
            get { return Context as UserContext; }
        }
    }
}
