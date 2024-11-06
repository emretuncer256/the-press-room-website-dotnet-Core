using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.DataAccess.EntityFramework;
using TPR.DataAccess.Abstract;
using TPR.DataAccess.Concrete.EntityFramework.Context;
using TPR.Entities.Concrete;

namespace TPR.DataAccess.Concrete.EntityFramework
{
    public class EfSubscriptionDal : EfEntityRepositoryBase<Subscription, ThePressRoomDBContext>, ISubscriptionDal
    {
    }
}
