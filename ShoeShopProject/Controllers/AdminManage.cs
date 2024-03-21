using Microsoft.AspNetCore.Mvc;

namespace ShoeShopProject.Controllers
{
    public class AdminManage : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
