using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{
    public class LatestNewsList : ViewComponent
    {
        IArticleService _articleService;
        public LatestNewsList(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var data = _articleService.GetLatestNews(10);
            return View(data.Data);
        }
    }
}
