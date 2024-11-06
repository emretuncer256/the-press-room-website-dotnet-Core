using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        ICategoryService _categoryService;
        public CategoryList(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _categoryService.GetCategories(21, true);
            return View(data.Data);
        }
    }
}
