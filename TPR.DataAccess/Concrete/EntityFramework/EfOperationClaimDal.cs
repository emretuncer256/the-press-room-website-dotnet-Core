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
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, ThePressRoomDBContext>, IOperationClaimDal
    {
    }
}
