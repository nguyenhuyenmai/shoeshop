using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using System.Diagnostics;

namespace ShoeShopProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly DBContext _context;

        public HomeController(ILogger<HomeController> logger, DBContext context)
		{
			_logger = logger;
			_context = context;
        }

		/// <summary>
		/// Gọi đến màn hình home page
		/// </summary>
		/// <returns></returns>
		public IActionResult HomePage()
		{
			try
			{
				// Mapping header
				ServiceMapping mapping = new ServiceMapping(_context, HttpContext);
				mapping.MappingHeader(this);

				// Khai báo service
				ProductService productService = new ProductService(_context);
				PropertyService propertyService = new PropertyService(_context);

				// Lấy các thông tin hiển thị
				List<Color> listColors = propertyService.GetAllColors();
				List<Size> listSizes = propertyService.GetAllSize();
                List<Brand> listBrands = propertyService.GetAllBrand();
                List<Slider> listSliders = propertyService.GetAllSliderActive();
                List<Product> listProducts = productService.GetAllProducts();

				// Truyền thông tin hiển thị vào view
				ViewBag.ListProducts = listProducts;
				ViewBag.ListColors = listColors;
				ViewBag.ListSizes = listSizes;
                ViewBag.ListBrands = listBrands;
                ViewBag.ListSliders = listSliders;
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.ToString());
            }

            return View();
		}
	}
}
