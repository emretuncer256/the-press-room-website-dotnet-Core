using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TPR.MVC.Areas.Author.Controllers
{
    [Area("Author")]
    [Authorize(Roles = "Author")]
    public class DashboardController : Controller
    {
        public IActionResult Index() => View();
    }
}
