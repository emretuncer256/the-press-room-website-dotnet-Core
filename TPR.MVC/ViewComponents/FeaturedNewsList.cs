using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{
    public class FeaturedNewsList : ViewComponent
    {
        IArticleService _articleService;
        public FeaturedNewsList(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _articleService.GetFeaturedNews(10, true);
            return View(data.Data);
        }
    }
}
