using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using ShoeShop.Models;
using ShoeShop.Service;
using ShoeShop.Statics;

namespace ShoeShop.Controllers
{
	public class ProductController : Controller
	{
		private readonly ILogger<ProductController> _logger;
		private readonly ShoeService _shoeService;
		private readonly BrandService _brandService;
		private readonly ShoeVariantService _shoeVariantService;
		private readonly ColorService _colorService;
		private readonly FeedbackService _feedbackService;
		private readonly DataContext _context;
		public ProductController(ILogger<ProductController> logger, DataContext dataContext)
		{
			_shoeService = new ShoeService(dataContext);
			_brandService = new BrandService(dataContext);
			_shoeVariantService = new ShoeVariantService(dataContext);
			_colorService = new ColorService(dataContext);
			_feedbackService = new FeedbackService(dataContext);
			_logger = logger;
			_context = dataContext;
		}

		public IActionResult Index(int? page, int? brand, string? search, string? sortOrder, int? shoeLineId)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);
			int _page;
			if (page == null || page.Value <= 0)
			{
				_page = 1;
			}
			else
			{
				_page = page.Value;
			}
			//Lấy tổng số sản phẩm theo tìm kiếm
			int shoeCount = _shoeService.GetProductCount(brand, search, shoeLineId);
			int totalPage;
			bool _sortOrder = true;
			int _brand = 0;
			if (sortOrder != null)
			{
				_sortOrder = false;
			}
			if (brand.HasValue)
			{
				_brand = brand.Value;
			}
			//Lấy số trang
			if (shoeCount % 12 == 0)
			{
				totalPage = shoeCount / 12;
			}
			else
			{
				totalPage = shoeCount / 12 + 1;
			}
			ViewBag.brandList = _brandService.GetBrandList();
			ViewBag.shoeList = _shoeService.GetShoeListPaging(_page, brand, search, sortOrder, shoeLineId);
			ViewBag.totalPage = totalPage;
			ViewBag.page = _page;
			ViewBag.gap = 2;
			ViewBag.sortOrder = _sortOrder;
			ViewBag.brand = _brand;
			ViewBag.search = search;
			return View();
		}
		public IActionResult ProductDetail(int? id, int? colorId)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);
			try
			{
				Shoe shoe = _shoeService.GetShoeById((int)id);
				shoe.ShoeImages = _shoeService.GetShoeimageListByShoeId((int)id);
				shoe.Feedbacks = _feedbackService.GetFeedbackByShoeId((int)id);
				ViewBag.shoe = shoe;
				ViewBag.brandList = _brandService.GetBrandList();
				ViewBag.brand = _brandService.GetBrandFromShoelineId(shoe.ShoeLineId);
				var color = _colorService.GetColorByShoeId((int)id);
				ViewBag.color = color;
				if (colorId != null)
				{
					ViewBag.ShoeVariant = _shoeVariantService.GetShoeVariantsByShoeIdAndColor((int)id, (int)colorId);
					ViewBag.colorId = colorId;
				}
				else
				{
					colorId = color.First().ColorId;
					ViewBag.colorId = (int)colorId;
					ViewBag.ShoeVariant = _shoeVariantService.GetShoeVariantsByShoeIdAndColor((int)id, (int)colorId); ;
				}
				return View();
			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
