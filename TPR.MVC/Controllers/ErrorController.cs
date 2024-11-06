using Microsoft.AspNetCore.Mvc;

namespace TPR.MVC.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult Error404()
        {
            return View();
        }
        [Route("403")]
        public IActionResult Error403()
        {
            return View();
        }
        [Route("{statusCode:int}")]
        public IActionResult Error(int statusCode)
        {
            return View();
        }
    }
}
