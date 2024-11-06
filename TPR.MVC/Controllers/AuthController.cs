using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPR.Business.Abstract;
using TPR.Core.Entities.Concrete;
using TPR.Entities.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Cryptography;

namespace TPR.MVC.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        IAuthService _authService;
        IUserService _userService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.Login(user);
                var loginUser = result.Data;
                if (loginUser != null && result.Success)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    var claims = _authService.GetClaims(loginUser).Data;
                    identity.AddClaims(claims);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                TempData["ErrorMessage"] = "Invalid email or password";
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserForRegisterDto user)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.Register(user);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Auth");
                }
                TempData["ErrorMessage"] = result.Message;
                return View("Index");
            }
            return View("Index");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["LogoutMessage"] = "You have successfully logged out!";
                return RedirectToAction("Index", "Auth");
            }
            return RedirectToAction("Index", "Home");

        }
        public IActionResult MyAccount()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
