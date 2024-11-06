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
    public class NewsletterMailManager : INewsletterMailService
    {
        INewsletterMailDal _newsletterMailDal;

        public NewsletterMailManager(INewsletterMailDal newsletterMailDal)
        {
            _newsletterMailDal = newsletterMailDal;
        }

        public IResult SubscribeMail(NewsletterMail mail)
        {
            if (_newsletterMailDal.Get(x => x.Email == mail.Email) != null)
            {
                return new ErrorResult("Already subscribed to this mail");
            }
            _newsletterMailDal.Add(mail);
            return new SuccessResult("Subscribed with the email");
        }

        public IResult UnsubscribeMail(NewsletterMail mail)
        {
            if (_newsletterMailDal.Get(x => x.Email == mail.Email) != null)
            {
                _newsletterMailDal.Delete(mail);
                return new SuccessResult("Email has been removed successfully");
            }
            return new ErrorResult("Email not found");
        }
    }
}
