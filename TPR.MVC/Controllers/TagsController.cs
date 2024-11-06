using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.Controllers
{
    public class TagsController : Controller
    {
        ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {

            return View(_tagService.GetTags(10, true).Data);
        }
    }
}
