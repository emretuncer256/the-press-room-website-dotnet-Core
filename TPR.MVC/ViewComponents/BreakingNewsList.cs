using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{
    public class BreakingNewsList : ViewComponent
    {
        IArticleService _articleService;
        public BreakingNewsList(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _articleService.GetBreakingNews(6);
            return View(data.Data);
        }
    }
}
