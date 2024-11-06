using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Abstract
{
    public interface IContactService
    {
        IResult Add(Contact contact);
        IResult Delete(int id);
        IDataResult<Contact> GetById(int id);
        IDataResult<List<Contact>> GetAll();
    }
}
