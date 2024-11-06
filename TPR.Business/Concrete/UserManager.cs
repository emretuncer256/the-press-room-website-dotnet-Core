using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Business.Abstract;
using TPR.Core.Entities.Concrete;
using TPR.DataAccess.Abstract;

namespace TPR.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void AddUser(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string mail)
        {
            return _userDal.Get(x => x.Email == mail);
        }

        public void DeleteUser(User user)
        {
            _userDal.Delete(user);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void UpdateUser(User user)
        {
            _userDal.Update(user);
        }

        public void AddRole(User user, OperationClaim claim)
        {
            _userDal.AddRoleToUser(user, claim);
        }

        public User GetById(int id)
        {
            return _userDal.Get(x => x.Id == id);
        }

        public List<User> GetAllUsers()
        {
            return _userDal.GetAll();
        }

        public List<User> GetAdmins()
        {
            var allusers = _userDal.GetAll();
            var admins = new List<User>();
            foreach (var user in allusers)
            {
                if (GetClaims(user).Any(x=>x.Id==1))
                {
                    admins.Add(user);
                }
            }
            return admins;
        }

        public List<User> GetAuthors()
        {
            var allusers = _userDal.GetAll();
            var authors = new List<User>();
            foreach (var user in allusers)
            {
                if (GetClaims(user).Any(x => x.Id == 2))
                {
                    authors.Add(user);
                }
            }
            return authors;
        }
    }
}
