using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Business.Abstract;
using TPR.Core.Entities.Concrete;
using TPR.Core.Utilities.Results;
using TPR.Core.Utilities.Results.Abstract;
using TPR.DataAccess.Abstract;

namespace TPR.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            var data = _operationClaimDal.GetAll();
            return new SuccessDataResult<List<OperationClaim>>(data);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            var data = _operationClaimDal.Get(x => x.Id == id);
            return new SuccessDataResult<OperationClaim>(data);
        }

        public IDataResult<OperationClaim> GetByName(string name)
        {
            var data = _operationClaimDal.Get(x => x.Name == name);
            return data == null
                ? new SuccessDataResult<OperationClaim>(data)
                : new ErrorDataResult<OperationClaim>(data, "Claim does not exists");
        }
    }
}
