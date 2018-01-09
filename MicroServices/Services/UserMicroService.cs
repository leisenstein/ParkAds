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
            return userUnitOfWork.Repository.GetAll();
        }

        public User GetById(int id)
        {
            return userUnitOfWork.Repository.Get(id);
        }

        public User GetByEmail(string email)
        {
            return userUnitOfWork.Repository.Find(user => user.Email.Equals(email)).FirstOrDefault();
        }

        public IEnumerable<User> GetByFirstName(string firstName)
        {
            return userUnitOfWork.Repository.Find(user => user.FirstName.StartsWith(firstName));
        }

        public IEnumerable<User> GetByLastName(string lastName)
        {
            return userUnitOfWork.Repository.Find(user => user.LastName.StartsWith(lastName));
        }

        public bool Add(User user)
        {
            if(GetByEmail(user.Email) == null)
            {
                userUnitOfWork.Repository.Add(user);
                return userUnitOfWork.Complete() > 0;
            }

            return false;
        }
    }
}
