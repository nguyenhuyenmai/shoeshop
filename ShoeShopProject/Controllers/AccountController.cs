using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ShoeShopProject.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [Route("login-admin")]
        [AllowAnonymous]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [Route("login")]
        [AllowAnonymous]
        public IActionResult LoginUser()
        {
            return View();
        }

        [Route("account-profile")]
        //[Authorize]
        public IActionResult AccountProfile()
        {
            return View();
        }

        [Route("google-login")]
        [AllowAnonymous]
        public IActionResult GoogleLogin(string ReturnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginCallback", new {returnUrl = ReturnUrl })
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);

        }

        [Route("LoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(string ReturnUrl)
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                var errorMessage = "Đăng nhập không thành công! Vui lòng kiểm tra thông tin đăng nhập";
                return RedirectToAction("Login", "Account", new { message = errorMessage });
            }

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl); // Chuyển hướng đến returnUrl
            }

            // Redirect to Homepage
            return RedirectToAction("HomePage", "Home");
        }
    }
}
