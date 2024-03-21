using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Controllers
{
    [Route("Shoping")]
    public class ShopingController : Controller
    {
        private readonly ILogger<ShopingController> _logger;
        private readonly DBContext _context;

        public ShopingController(ILogger<ShopingController> logger, DBContext context)
        {
            _logger = logger;
            _context=context;
        }

        [Route("Cart")]
        public IActionResult Cart()
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);

            EmrSession emrSession = new EmrSession(HttpContext);
            int userID = emrSession.userId;

            CartService cartService = new CartService(_context);
            List<CartDetails> listCartDetails = cartService.GetUserCartDetails(userID);
            ViewBag.ListCartDetails = listCartDetails;

            return View();
        }
        public IActionResult CartCheckout()
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);
            return View();
        }

        public IActionResult OderDetails()
        {
            ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
            mapping.MappingHeader(this);
            return View();
        }

        [Route("AddToCart")]
        public IActionResult AddToCart(int shoeVariantId, int quantity)
        {
            // add product to cart
            EmrSession emrSession = new EmrSession(HttpContext);
            int userID = emrSession.userId;

            if (shoeVariantId >= 0)
            {
                CartService cartService = new CartService(_context);
                int status = cartService.AddToCart(shoeVariantId, quantity, userID);
                if (status < 0)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = true });
        }
    }
}
