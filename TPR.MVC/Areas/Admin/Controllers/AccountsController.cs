using Microsoft.AspNetCore.Mvc;
using TPR.Business.Abstract;

namespace TPR.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        IUserService _userService;
        IAuthService _authService;

        public AccountsController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public IActionResult AllUsers()
        {
            var data = _userService.GetAllUsers();
            return View(data);
        }
        public IActionResult Admins()
        {
            return View(_userService.GetAdmins());
        }
        public IActionResult Writers()
        {
            return View(_userService.GetAuthors());
        }
    }
}
