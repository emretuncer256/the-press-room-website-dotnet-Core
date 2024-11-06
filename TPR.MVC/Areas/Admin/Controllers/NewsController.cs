using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        IArticleService _articleManager;
        public NewsController(IArticleService articleManager)
        {
            _articleManager = articleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = _articleManager.GetAll().Data.OrderByDescending(x => x.CreatedAt).ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult ViewNews(int id)
        {
            var data = _articleManager.GetById(id);
            return View(data);
        }
        [HttpGet]
        public IActionResult PendingNews()
        {
            var data = _articleManager.GetNotApprovedNews().Data.OrderByDescending(x => x.CreatedAt);
            return View(data.ToList());
        }
        [HttpPost]
        public JsonResult ApproveNews(int id)
        {
            return Json(_articleManager.ApproveNews(id));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = _articleManager.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public JsonResult ToggleFeaturedNews(int id)
        {
            var result = _articleManager.ToggleFeaturedNews(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult ToggleBreakingNews(int id)
        {
            var result = _articleManager.ToggleBreakingNews(id);
            return Json(result);
        }
    }
}
