using System.Collections.Generic;
using DataAccess.DB.UnitOfWork;
using DataAccess.DB.Contexts;
using Domain;
using System.Linq;

namespace DataAccess
{
    public class UserMicroService
    {
        private UserUnitOfWork userUnitOfWork = new UserUnitOfWork(new UserContext());
        public IEnumerable<User> GetAll()
        {
            return userUnitOfWork.Users.GetAll();
        }

        public User GetById(int id)
        {
            return userUnitOfWork.Users.Get(id);
        }

        public User GetByEmail(string email)
        {
            return userUnitOfWork.Users.Find(user => user.Email.Equals(email)).FirstOrDefault();
        }

        public IEnumerable<User> GetByFirstName(string firstName)
        {
            return userUnitOfWork.Users.Find(user => user.FirstName.StartsWith(firstName));
        }

        public IEnumerable<User> GetByLastName(string lastName)
        {
            return userUnitOfWork.Users.Find(user => user.LastName.StartsWith(lastName));
        }

        public bool Add(User user)
        {
            if(GetByEmail(user.Email) == null)
            {
                userUnitOfWork.Users.Add(user);
                return userUnitOfWork.Complete() > 0;
            }

            return false;
        }
    }
}
