using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using TPR.Business.Abstract;
using TPR.Core.Entities.Concrete;
using TPR.Core.Utilities.Results;
using TPR.Entities.Concrete;
using TPR.MVC.Areas.Author.Models;
using TPR.MVC.Enums;
using TPR.MVC.Helpers;

namespace TPR.MVC.Areas.Author.Controllers
{
    [Area("Author")]
    [Authorize(Roles = "Author")]
    public class NewsController : Controller
    {
        IArticleService _articleService;
        ITagService _tagService;
        int _id;
        public NewsController(IArticleService articleService, ITagService tagService)
        {
            _articleService = articleService;
            _tagService = tagService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out _id);

            var data = _articleService.GetAllByAuthorId(_id).Data;
            return View(data.OrderByDescending(x => x.CreatedAt).ToList());
        }
        [HttpGet]
        public IActionResult WriteNews() => View();
        [HttpPost]
        public async Task<IActionResult> WriteNews(NewsUploadModel news)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out _id);

                var article = new Article
                {
                    UserId = _id,
                    CategoryId = news.CategoryId,
                    Title = news.Title,
                    Content = news.Content,
                    CreatedAt = DateTime.Now,
                    Status = false
                };
                string thumbnail = await ImageHelper.UploadImage(news.Thumbnail, UploadPaths.AuthorThumbnails);
                article.Thumbnail = thumbnail;

                var result = _articleService.AddWithTags(article, news.Tags);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = $"{article.Title} has been saved successfully.";
                    return RedirectToAction("Index");
                }
                TempData["ErrorMessage"] = result.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult ViewNews(int id)
        {
            if (CheckIsAuthorHasTheArticle(id))
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out _id);
                var article = _articleService.GetById(id);
                return View(article.Data);
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult EditNews(int id)
        {
            if (CheckIsAuthorHasTheArticle(id))
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out _id);
                var article = _articleService.GetById(id);
                var newsModel = NewsUploadModel.FromArticle(article.Data, _articleService);
                return View(newsModel);
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditNews(NewsUploadModel news)
        {
            if (CheckIsAuthorHasTheArticle(news.ArticleId))
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out _id);
                var article = _articleService.GetById(news.ArticleId).Data;

                article.Content = news.Content;
                article.Title = news.Title;
                article.CategoryId = news.CategoryId;
                article.UpdatedAt = DateTime.Now;

                if (news.Thumbnail != null)
                {
                    ImageHelper.DeleteImage(Path.Combine(UploadPaths.AuthorThumbnails, article.Thumbnail));
                    article.Thumbnail = await ImageHelper.UploadImage(news.Thumbnail, UploadPaths.AuthorThumbnails);
                }
                _articleService.UpdateWithTags(article, news.Tags);
                return RedirectToAction("ViewNews", new { id = article.Id });
            }
            return View(news);
        }
        [HttpPost]
        public JsonResult PublishNews(int id)
        {
            return CheckIsAuthorHasTheArticle(id) ? Json(_articleService.PublishArticle(id)) : Json("");
        }
        [HttpPost]
        public JsonResult UnpublishNews(int id)
        {
            return CheckIsAuthorHasTheArticle(id) ? Json(_articleService.UnpublishArticle(id)) : Json("");
        }
        [HttpPost]
        public JsonResult DeleteNews(int id)
        {
            if (CheckIsAuthorHasTheArticle(id))
            {
                var article = _articleService.GetById(id).Data;
                if (!article.IsPublished)
                {
                    _articleService.DeleteAllTags(article);
                    var result = _articleService.Delete(article);
                    ImageHelper.DeleteImage(Path.Combine(UploadPaths.AuthorThumbnails, article.Thumbnail));
                    return Json(result);
                }
                return Json(new ErrorResult("Please unpublish the news, then you can delete it."));
            }
            return Json("");
        }
        private bool CheckIsAuthorHasTheArticle(int articleId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out _id);
            var article = _articleService.GetById(articleId);
            if (article.Data == null || !article.Success || article.Data.UserId != _id) return false;
            return true;
        }
    }
}
