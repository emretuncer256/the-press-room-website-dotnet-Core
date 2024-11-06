using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using TPR.Business.Abstract;
using TPR.Entities.Concrete;

namespace TPR.MVC.Controllers
{
    public class NewsController : Controller
    {
        IArticleService _articleService;

        public NewsController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Read(int id)
        {
            var article = _articleService.GetNewsById(id).Data;
            _articleService.NewsHited(article);
            return View(article);
        }
        [HttpPost]
        public IActionResult MakeComment(Comment comment)
        {
            int userId;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            if (!comment.Text.IsNullOrEmpty() && userId != null)
            {
                comment.UserId = userId;
                comment.CreatedAt = DateTime.Now;
                _articleService.MakeComment(comment);
                return RedirectToAction("Read", new { id = comment.ArticleId });
            }
            return View();
        }
    }
}
