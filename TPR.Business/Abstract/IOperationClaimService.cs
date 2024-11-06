using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Entities.Concrete;
using TPR.Core.Utilities.Results.Abstract;

namespace TPR.Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<OperationClaim> GetById(int id);
        IDataResult<OperationClaim> GetByName(string name);
        IDataResult<List<OperationClaim>> GetAll();
    }
}
