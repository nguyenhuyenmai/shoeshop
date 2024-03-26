using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ShoeShopProject.Controllers
{
    [Route("AdminManage")]
    public class AdminManageController : Controller
    {
        private readonly ILogger<AdminManageController> _logger;
        private readonly DBContext _context;

        public AdminManageController(ILogger<AdminManageController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("MeDashboard")]
        public IActionResult MeDashboard()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AdminAccountList")]
        public IActionResult AdminAccountList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            List<Admin> admins = _context.Admins.ToList();
            ViewBag.ListAdmin = admins;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AddAdminAccount")]
        public IActionResult AddAdminAccount()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AccountAdminProfile")]
        public IActionResult AccountAdminProfile(int adminID)
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.Admin = _context.Admins.FirstOrDefault(ad => ad.Id == adminID);
            
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("UpdateAdminAccountProfile")]
        public IActionResult UpdateAdminAccountProfile(int adminID)
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            Admin admin = _context.Admins.FirstOrDefault(ad => ad.Id == adminID);
            if (admin != null)
            {
                ViewBag.Admin = admin;
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("DeleteAdminAccount")]
        public IActionResult DeleteAdminAccount(int adminID)
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            Admin admin = _context.Admins.FirstOrDefault(ad => ad.Id == adminID);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AddNewAdminAccount")]
        public IActionResult AddNewAdminAccount(string fullName, string userName, string phone, string password, int role, IFormFile userImg)
        {
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
            {
                var imagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile");
                Directory.CreateDirectory(imagePath);

                Admin admin = _context.Admins.FirstOrDefault(ad => ad.Username == userName);
                if (admin == null)
                {
                    admin = new Admin();

                    string oldImg = admin.Image;
                    string usrImagePath = String.Empty;
                    if (userImg != null && userImg.Length > 0)
                    {
                        string fileNameWithoutExtension = $"profile_admin_{DateTime.Now:yyyyMMdd_HHmmss}";
                        string fileExtension = Path.GetExtension(userImg.FileName);
                        string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                        // Save the image to the directory
                        imagePath = Path.Combine(imagePath, fileName);

                        usrImagePath = $"/assets/img/account/profile/{fileName}";
                        admin.Image = usrImagePath;
                    }

                    admin.Username = userName;
                    admin.Password = password;
                    admin.Fullname = fullName;
                    admin.Phone = phone;
                    admin.RoleId =  role;

                    _context.Admins.Add(admin);
                    _context.SaveChanges();

                    if (userImg != null && !string.IsNullOrEmpty(imagePath))
                    {
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            userImg.CopyTo(stream);
                        }
                        if (!string.IsNullOrEmpty(oldImg))
                        {
                            var oldImagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile", Path.GetFileName(oldImg));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                    }

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("UpdateAdminAccount")]
        public IActionResult UpdateAdminAccount(int adminID, string fullName, string userName, string phone, string password, int role, IFormFile userImg)
        {
            if (adminID >= 0)
            {

                var imagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile");
                Directory.CreateDirectory(imagePath);

                Admin admin = _context.Admins.FirstOrDefault(ad => ad.Id == adminID);
                // Check same account
                Admin admin1 = _context.Admins.FirstOrDefault(ad => ad.Username == userName);

                if (admin != null)
                {
                    if (admin1 != null && admin.Id != admin1.Id)
                    {
                        return Json(new { success = false });
                    }

                    string oldImg = admin.Image;
                    string usrImagePath = String.Empty;
                    if (userImg != null && userImg.Length > 0)
                    {
                        string fileNameWithoutExtension = $"profile_admin_{DateTime.Now:yyyyMMdd_HHmmss}";
                        string fileExtension = Path.GetExtension(userImg.FileName);
                        string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                        // Save the image to the directory
                        imagePath = Path.Combine(imagePath, fileName);

                        usrImagePath = $"/assets/img/account/profile/{fileName}";
                        admin.Image = usrImagePath;
                    }

                    admin.Username = userName;
                    admin.Password = password;
                    admin.Fullname = fullName;
                    admin.Phone = phone;
                    admin.RoleId =  role;

                    _context.Admins.Update(admin);
                    _context.SaveChanges();

                    if (userImg != null && !string.IsNullOrEmpty(imagePath))
                    {
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            userImg.CopyTo(stream);
                        }
                        if (!string.IsNullOrEmpty(oldImg))
                        {
                            var oldImagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile", Path.GetFileName(oldImg));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                    }

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        public IActionResult ContactManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            return View();
        }


    }
}
