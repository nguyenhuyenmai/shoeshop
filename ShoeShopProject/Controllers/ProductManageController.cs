using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.Services;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Controllers
{
    [Route("ProductManage")]
    public class ProductManageController : Controller
    {
        private readonly ILogger<ProductManageController> _logger;
        private readonly DBContext _context;

        public ProductManageController(ILogger<ProductManageController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ProductManageList")]
        public IActionResult ProductManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ProductService productService = new ProductService(_context);
            List<ProductManageView> listProduct = productService.GetListProductManage();

            ViewBag.ProductList = listProduct;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("AddProductManage")]
        public IActionResult AddProductManage()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.ListCategories = _context.Categories.ToList();
            ViewBag.ListBrand = _context.Brands.ToList();


            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCategory"></param>
        /// <param name="productBrand"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="productDesc"></param>
        /// <param name="productImg"></param>
        /// <returns></returns>
        [Route("AddNewProduct")]
        public IActionResult AddNewProduct(int productCategory, int productBrand, string productName, int productPrice, string productDesc, IFormFile productImg)
        {
            string imgPath = String.Empty;
            if (productImg != null && productImg.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot", "images", "img", "products");
                Directory.CreateDirectory(imagePath);

                string fileNameWithoutExtension = $"product_{DateTime.Now:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(productImg.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";
                imgPath = $"/images/img/products/{fileName}";

                Product product = new Product()
                {
                    BrandId = productBrand,
                    CategoryId = productCategory,
                    Name = productName,
                    Price = Convert.ToDecimal(productPrice),
                    Desciption = productDesc,
                    Thumbnail = imgPath
                };
                _context.Products.Add(product);
                _context.SaveChanges();

                if (productImg != null && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        productImg.CopyTo(stream);
                    }
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

            public IActionResult PropertyManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);
            return View();
        }

        
        public IActionResult SliderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);
            return View();
        }
    }
}
