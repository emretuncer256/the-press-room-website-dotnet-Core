using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{
    public class ArticleCommentList : ViewComponent
    {
        IArticleService _articleService;
        public ArticleCommentList(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke(int id)
        {
            var data = _articleService.GetComments(_articleService.GetById(id).Data);
            return View(data.Data);
        }
    }
}
