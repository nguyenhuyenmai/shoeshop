using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Models;

namespace ShoeShopProject.Data
{
    /// <summary>
    /// Mapping các service
    /// </summary>
    public class ServiceMapping
    {
        private readonly DBContext _context;
        private readonly HttpContext _httpContext;

        public ServiceMapping(DBContext context, HttpContext httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Cấu hình dữ liệu cho header
        /// </summary>
        public void MappingHeader(Controller controller)
        {
            try
            {
                EmrSession emrSession = new EmrSession(_httpContext);
                bool checkLogin = false;
                if (emrSession != null && emrSession.userId > -1)
                {
                    User user = _context.Users.FirstOrDefault(x => x.Id == emrSession.userId);
                    checkLogin = true;
                    controller.ViewBag.User = user;
                }
                controller.ViewBag.CheckLogin = checkLogin;
                controller.ViewBag.EmrSession = emrSession;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
