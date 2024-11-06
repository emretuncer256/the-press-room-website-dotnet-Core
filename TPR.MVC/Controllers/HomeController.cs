using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TPR.Business.Abstract;
using TPR.Entities.Concrete;
using TPR.MVC.Models;

namespace TPR.MVC.Controllers
{
    public class HomeController : Controller
    {
        IContactService _cs;
        ICategoryService _cat;
        INewsletterMailService _nlms;
        public HomeController(IContactService cs, ICategoryService cat, INewsletterMailService nlms)
        {
            _cs = cs;
            _cat = cat;
            _nlms = nlms;
        }

        public IActionResult Index() => View();
        public IActionResult Category()
        {
            var data = _cat.GetAll().Data.OrderBy(x => x.Name).ToList();
            return View(data);
        }
        public IActionResult About() => View();
        public IActionResult Contact() => View();
        [HttpPost]
        public JsonResult ContactUs(Contact contact)
        {
            contact.CreatedAt = DateTime.Now;
            contact.Status = true;
            contact.Read = false;
            return Json(_cs.Add(contact));
        }
        [HttpPost]
        public JsonResult SubscribeNewsletter(NewsletterMail mail)
        {
            return Json(_nlms.SubscribeMail(mail));
        }
    }
}