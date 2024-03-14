using Microsoft.AspNetCore.Mvc;
using ShoeShop.Data;
using ShoeShop.Service;
using ShoeShop.Models;
using System.Collections;
using ShoeShop.ViewModels;
using ShoeShop.Statics;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System;

namespace ShoeShop.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _context;
        private readonly MailService _mailService;
        public UserController(ILogger<UserController> logger, DataContext dataContext, MailService mailService)
        {
            this._logger = logger;
            this._context = dataContext;
            _mailService = mailService;
        }

        [Route("ListCustomer")]
        [Authorize(Roles = "Admin")]
        public IActionResult ListCustomer()
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            UserService userService = new UserService(_context);
            ArrayList listCustomer = userService.ListCustomer();
            ViewBag.listCustomer = listCustomer;
            return View();
        }


        [Route("ListUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult ListUser()
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            UserService userService = new UserService(_context);
            ArrayList listUser = userService.ListUser();
            ViewBag.listUser = listUser;
            return View();
        }


        [Route("CustomerDetail")]
        [Authorize(Roles = "Admin")]
        public IActionResult CustomerDetail(string userId)
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            UserService userService = new UserService(_context);
            CustomerModel customer = new CustomerModel();
            customer = userService.getCustomerProfile(Convert.ToInt32(userId));
            ViewBag.customer = customer;
            return View();
        }

        [Route("UpdateCustomer")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCustomer(IFormFile uploadedImage, string customerId)
        {
            if (!string.IsNullOrEmpty(customerId))
            {
                int id = int.Parse(customerId);
                string name = Request.Form["name"];
                string email = Request.Form["email"];
                string phone = Request.Form["phone"];
                string address = Request.Form["address"];
                int gender = int.Parse(Request.Form["gender"]);
                DateTime updateDate = DateTime.Now;
                string imagePath = "";
                string imgPath = "";
                if (uploadedImage != null && uploadedImage.Length > 0)
                {
                    imgPath = Path.Combine("wwwroot", "assets", "img", "user");
                    Directory.CreateDirectory(imgPath);
                    string imgNameWithoutExtension = $"user{DateTime.Now:yyyyMMdd_HHmmss}";
                    string imgExtension = Path.GetExtension(uploadedImage.FileName);
                    string imgName = $"{imgNameWithoutExtension}{imgExtension}";
                    imgPath = Path.Combine(imgPath, imgName);

                    imagePath = $"/assets/img/user/{imgName}";
                }
                UserService userService = new UserService(_context);
                userService.updateCustomer(id, imagePath, name, email, gender, address, phone, updateDate);

                if (!string.IsNullOrEmpty(imgPath))
                {
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        uploadedImage.CopyTo(stream);
                    }

                }

                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [Route("AddUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser()
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            UserService userService = new UserService(_context);
            List<Role> listRole = userService.ListRole();
            ViewBag.ListRole = listRole;
            return View();
        }

        [Route("AddCustomer")]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [Route("DoAddUser")]
        public IActionResult DoAddUser(string email, int roleId, string name, string phone)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            UserService userService = new UserService(_context);

            if (!string.IsNullOrEmpty(email))
            {
                User user = userService.GetUserByEmail(email);
                // Neu user da co trong he thong
                if (user != null)
                {
                    // Neu user da la staff thi hien thi thong bao
                    if (user.RoleId != 5)
                    {
                        return Json(new { success = false, exist = true });
                    }
                    // Neu la customer thi chuyen doi role
                    userService.UpdateUsrRole(user.UserId, roleId);

                    return Json(new { success = true });
                }

                // Thuc hien them moi neu chua ton tai
                string password = userService.GenerateRandomPassword();
                userService.AddNewStaff(email, roleId, name, phone, password);

                string emailContent = $"Chúc mừng bạn đã được quản trị viên phê duyệt quyền staff cho email của bạn. Đăng nhập vào hệ thống \"{Url.Action("Index", "Login", new {}, protocol: Request.Scheme)}\" với email: {email} và password: {password}";
                _mailService.SendToEmail(email, "Chào mừng đến với EShop", emailContent);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("DoAddCustomer")]
        public IActionResult DoAddCustomer(IFormFile uploadedImage)
        {
            string name = Request.Form["name"];
            string email = Request.Form["email"];
            string phone = Request.Form["phone"];
            string password = Request.Form["password"];
            string address = Request.Form["address"];
            int gender = int.Parse(Request.Form["gender"]);
            string statusValue = Request.Form["status"];
            bool status = (statusValue == "1");
            int roleId = 5;
            DateTime createDate = DateTime.Now;
            DateTime updateDate = DateTime.Now;
            string imagePath = "";
            string imgPath = "";
            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                imgPath = Path.Combine("wwwroot", "assets", "img", "user");
                Directory.CreateDirectory(imgPath);
                string imgNameWithoutExtension = $"user{DateTime.Now:yyyyMMdd_HHmmss}";
                string imgExtension = Path.GetExtension(uploadedImage.FileName);
                string imgName = $"{imgNameWithoutExtension}{imgExtension}";
                imgPath = Path.Combine(imgPath, imgName);


                imagePath = $"/assets/img/user/{imgName}";
            }


            UserService userService = new UserService(_context);
            userService.addNewCustomer(roleId, email, name, phone, password, address, gender, imagePath, createDate, updateDate, status);
            int cusId = userService.getLastestCustomerId();
            int custatus = 1;
            userService.addNewCustomerStatus(cusId, custatus);
            if (!string.IsNullOrEmpty(imgPath))
            {
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    uploadedImage.CopyTo(stream);
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }


        [Route("UserProfile")]
        public IActionResult UserProfile(string userId)
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            UserService userService = new UserService(_context);
            User user = new User();
            user = userService.getUserProfile(Convert.ToInt32(userId));
            ViewBag.User = user;
            return View();
        }

        [Route("ManageUserProfile")]
        public IActionResult ManageUserProfile(string userId)
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            UserService userService = new UserService(_context);
            User user = new User();
            user = userService.getUserProfile(Convert.ToInt32(userId));
            ViewBag.User = user;
            return View();
        }


        [Route("UpdateUserRole")]
        public IActionResult UpdateUserRole(int usrId)
        {
            UserService userService = new UserService(_context);
            User usr = userService.getUserProfile(usrId);

            int roleId = int.Parse(Request.Form["roleId"]);

            usr = userService.UpdateUsrRole(usrId, roleId);
            if (usr != null)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("UpdateUserProfile")]
        public IActionResult UpdateUserProfile(IFormFile usrImg)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            UserService userService = new UserService(_context);
            User usr = userService.getUserProfile(emrSession.userId);

            string usrName = Request.Form["usrName"];
            int usrGender = int.Parse(Request.Form["usrGender"]);
            string usrPhone = Request.Form["usrPhone"];
            string usrAddress = Request.Form["usrAddress"];

            string usrImagePath = "";
            var imagePath = "";
            string oldImg = usr.Image;
            if (usrImg != null && usrImg.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                imagePath = Path.Combine("wwwroot", "images", "avatar", "profile", usr.UserId.ToString());
                Directory.CreateDirectory(imagePath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"profile_{usr.UserId}_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(usrImg.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                imagePath = Path.Combine(imagePath, fileName);

                usrImagePath = $"/images/avatar/profile/{usr.UserId.ToString()}/{fileName}";
            }

            usr = userService.UpdateUsrProfile(emrSession.userId, usrName, usrImagePath, usrPhone, usrGender, usrAddress);

            if (usr != null)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        usrImg.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "images", "avatar", "profile", usr.UserId.ToString(), Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                // Add User Session
                emrSession = new EmrSession(HttpContext);
                emrSession.userId = usr.UserId;
                emrSession.Role = usr.RoleId;
                emrSession.Email = usr.Email;
                emrSession.userName = usr.UserName;
                emrSession.Image = string.IsNullOrEmpty(usr.Image) ? "" : usr.Image;
                emrSession.putSession(HttpContext);

                return Json(new { success = true });
            }


            return Json(new { success = false });
        }


        [Route("UserOrders")]
        public IActionResult UserOrders(int userID)
        {
            RouteMapping mapping = new RouteMapping();
            mapping.AddService(HttpContext, this, _context);

            OrderService orderService = new OrderService(_context);
            UserService userService = new UserService(_context);

            ArrayList listUserOrder = orderService.GetListUserOrder(userID);
            User customer = userService.getUserProfile(userID);

            ViewBag.ListUserOrder = listUserOrder;
            ViewBag.Customer = customer;

            return View();
        }

        
        [Route("UserOrderDetails")]
        public IActionResult UserOrderDetails(int orderID)
        {
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			OrderService orderService = new OrderService(_context);
			UserService userService = new UserService(_context);
			EmrSession emrSession = new EmrSession(HttpContext);

			Order order = orderService.GetOrderByID(orderID);
			ArrayList listOrderItem = orderService.GetListOrderItem(orderID);
			User customer = userService.getUserProfile(emrSession.userId);

			if (order != null)
			{
				ViewBag.Order = order;
				ViewBag.ListOrderItem = listOrderItem;
				ViewBag.Customer = customer;
			}

			return View();
		}
    }
}
