using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.DataAccess.EntityFramework;
using TPR.Core.Entities.Concrete;
using TPR.DataAccess.Abstract;
using TPR.DataAccess.Concrete.EntityFramework.Context;

namespace TPR.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ThePressRoomDBContext>, IUserDal
    {
        public void AddRoleToUser(User user, OperationClaim claim)
        {
            using (var context = new ThePressRoomDBContext())
            {
                context.UserOperationClaims.Add(new UserOperationClaim { UserId = user.Id, OperationClaimId = claim.Id });
                context.SaveChanges();
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ThePressRoomDBContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
