using Microsoft.AspNetCore.Mvc;

namespace TPR.MVC.ViewComponents
{
    public class PopularNewsList:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
