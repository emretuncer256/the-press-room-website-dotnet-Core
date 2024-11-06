using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{
    public class BannerNewsList : ViewComponent
    {
        IArticleService _articleService;
        public BannerNewsList(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
