using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities.Concrete;
using TPR.Entities.Concrete;

namespace TPR.Business.Abstract
{
    public interface IUserService
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void AddRole(User user, OperationClaim claim);
        User GetByMail(string mail);
        User GetById(int id);
        List<OperationClaim> GetClaims(User user);
        List<User> GetAllUsers();
        List<User> GetAdmins();
        List<User> GetAuthors();
    }
}
