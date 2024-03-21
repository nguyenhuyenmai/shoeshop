using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ShoeShopProject.Models;
using ShoeShopProject.Data;
using ShoeShopProject.Services;

namespace ShoeShopProject.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DBContext _context;

        public AccountController(ILogger<AccountController> logger, DBContext context)
        {
            _logger = logger;
            this._context = context;
        }

        /// <summary>
        /// admin đăng nhập
        /// </summary>
        /// <returns></returns>
        [Route("login-admin")]
        [AllowAnonymous]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        /// <summary>
        /// Khách hàng đăng nhập
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [AllowAnonymous]
        public IActionResult LoginUser()
        {
            return View();
        }

        /// <summary>
        /// Profile người dùng
        /// </summary>
        /// <returns></returns>
        [Route("account-profile")]
        //[Authorize]
        public IActionResult AccountProfile(int userID)
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);

            if (userID >= 0)
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == userID);
                if (user != null)
                {
                    ViewBag.User = user;
                }
            }

            return View();
        }

        /// <summary>
        /// Đăng nhập bằng google
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sau khi đăng nhập bằng google
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [Route("LoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(string ReturnUrl)
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                // Kiểm tra đăng nhập nếu không thành công thì thông báo lỗi
                var errorMessage = String.Empty;
                if (!result.Succeeded)
                {
                    errorMessage = "Đăng nhập không thành công! Vui lòng kiểm tra thông tin đăng nhập";
                    return RedirectToAction("Login", "Account", new { message = errorMessage });
                }
                // Lấy giá trị email, name, img của người dùng khi đăng nhập
            
                var idClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                string idIdentity = idClaim != null ? idClaim.Value : String.Empty;
                // Kiểm tra claims
                if (String.IsNullOrEmpty(idIdentity))
                {
                    errorMessage = "Không thể đăng nhập bằng tài khoản của bạn. Vui lòng kiểm tra lại email";
                    return RedirectToAction("Login", "Account", new { message = errorMessage });

                } else {
                    // Khai báo biến
                    var emailClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
                    var nameClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
                    var imgUrlClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Uri);

                    string usrEmail = emailClaim != null ? emailClaim.Value : String.Empty;
                    string usrName = nameClaim != null ? nameClaim.Value : String.Empty;
                    string usrImage = imgUrlClaim != null ? imgUrlClaim.Value : String.Empty;
                    var user = new User();

                    if (!String.IsNullOrEmpty(usrEmail))
                    {
                        user = _context.Users.FirstOrDefault(x => x.Email == usrEmail);
                        // Nếu user chưa tồn tại thì add vào DB
                        if (user == null)
                        {
                            User newUser = new User
                            {
                                Email = usrEmail,
                                Fullname = usrName,
                                Address = String.Empty,
                                Phone = String.Empty,
                                Image = !String.IsNullOrEmpty(usrImage) ? usrImage : Constants.DEFAULT_IMG_USER,
                                Birthday = null
                            };
                            AccountService accountService = new AccountService(_context);
                            user = accountService.AddUser(newUser);
                        }

                        EmrSession emrSession = new EmrSession(HttpContext);
                        emrSession.userId = user.Id;
                        emrSession.userIdIdentity = idIdentity;
                        emrSession.userEmail = user.Email;
                        emrSession.userName = user.Fullname;
                        emrSession.userImage = user.Image != null ? user.Image : String.Empty;
                        emrSession.putSession(HttpContext);

                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl); // Chuyển hướng đến returnUrl
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Redirect to Homepage
            return RedirectToAction("HomePage", "Home");
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            EmrSession emrSession = new EmrSession(HttpContext);
            emrSession.clearSession(HttpContext);
            return RedirectToAction("HomePage", "Home");
        }
    }
}
