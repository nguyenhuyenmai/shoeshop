using Microsoft.AspNetCore.Mvc;

namespace ShoeShopProject.Controllers
{
    public class ShopingController : Controller
    {
        private readonly ILogger<ShopingController> _logger;

        public ShopingController(ILogger<ShopingController> logger)
        {
            _logger = logger;
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public IActionResult OderDetails()
        {
            return View();
        }
    }
}
