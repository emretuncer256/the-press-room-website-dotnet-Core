using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPR.Core.Utilities.Results.Abstract;
using TPR.Entities.Concrete;

namespace TPR.Business.Abstract
{
    public interface INewsletterMailService
    {
        IResult SubscribeMail(NewsletterMail mail);
        IResult UnsubscribeMail(NewsletterMail mail);
    }
}
