using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{

    public class TagList : ViewComponent
    {
        ITagService _tagService;
        public TagList(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _tagService.GetTags(10, true);
            return View(data.Data);
        }
    }
}
