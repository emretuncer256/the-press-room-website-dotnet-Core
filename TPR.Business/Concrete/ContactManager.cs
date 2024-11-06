using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Business.Abstract;
using TPR.Core.Utilities.Results;
using TPR.Core.Utilities.Results.Abstract;
using TPR.DataAccess.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Concrete
{
    public class ContactManager : IContactService
    {
        IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public IResult Add(Contact contact)
        {
            _contactDal.Add(contact);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var contact = _contactDal.Get(x => x.Id == id);
            if (contact != null)
            {
                _contactDal.Delete(contact);
                return new SuccessResult();
            }
            return new ErrorResult("Contact not found");
        }

        public IDataResult<List<Contact>> GetAll()
        {
            var data = _contactDal.GetAll();
            return new SuccessDataResult<List<Contact>>(data);
        }

        public IDataResult<Contact> GetById(int id)
        {
            var data = _contactDal.Get(x=>x.Id == id);
            return new SuccessDataResult<Contact>(data);
        }
    }
}
