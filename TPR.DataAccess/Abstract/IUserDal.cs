using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.DataAccess;
using TPR.Core.Entities.Concrete;

namespace TPR.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        void AddRoleToUser(User user, OperationClaim claim);
    }
}
