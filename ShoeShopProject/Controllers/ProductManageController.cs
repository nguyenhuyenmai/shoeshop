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
        /// <returns></returns>
        [Route("UpdateProductManage")]
        public IActionResult UpdateProductManage(int productID)
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.Product = _context.Products.FirstOrDefault(x => x.Id == productID);
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
        [Route("UpdateProductInfo")]
        public IActionResult UpdateProductInfo(int productID, int productCategory, int productBrand, string productName, int productPrice, string productDesc, IFormFile productImg)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == productID);
            if (product != null)
            {
                var imagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails");
                Directory.CreateDirectory(imagePath);

                string oldImg = product.Thumbnail;
                string imgThumbnails = String.Empty;

                if (productImg != null && productImg.Length > 0)
                {
                    string fileNameWithoutExtension = $"product_image_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(productImg.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);

                    imgThumbnails = $"/assets/img/products/thumbnails/{fileName}";
                    product.Thumbnail = imgThumbnails;
                }

                product.BrandId = productBrand;
                product.CategoryId = productCategory;
                product.Name = productName;
                product.Price = Convert.ToDecimal(productPrice);
                product.Desciption = productDesc;

                _context.Products.Update(product);
                _context.SaveChanges();

                if (productImg != null && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        productImg.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails", Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                return Json(new { success = true });
            }
            
            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteProductInfo")]
        public IActionResult DeleteProductInfo(int productID)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id ==  productID);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
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

            if (productImg != null && productImg.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails");
                Directory.CreateDirectory(imagePath);

                Product product = new Product();

                string oldImg = product.Thumbnail;
                string imgThumbnails = String.Empty;

                if (productImg != null && productImg.Length > 0)
                {
                    string fileNameWithoutExtension = $"product_image_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(productImg.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);

                    imgThumbnails = $"/assets/img/products/thumbnails/{fileName}";
                    product.Thumbnail = imgThumbnails;
                }

                product.BrandId = productBrand;
                product.CategoryId = productCategory;
                product.Name = productName;
                product.Price = Convert.ToDecimal(productPrice);
                product.Desciption = productDesc;

                _context.Products.Add(product);
                _context.SaveChanges();

                if (productImg != null && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        productImg.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "products", "thumbnails", Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("PropertyManageList")]
        public IActionResult PropertyManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            ViewBag.ListCategories = _context.Categories.ToList();
            ViewBag.ListBrand = _context.Brands.ToList();
            ViewBag.ListColor = _context.Colors.ToList();
            ViewBag.ListSize = _context.Sizes.ToList();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("DeleteProperty")]
        public IActionResult DeleteProperty(string property, int deleteID)
        {
            if (!String.IsNullOrEmpty(property) && deleteID >= 0)
            {
                if (Constants.PRO_CATEGORY.Equals(property))
                {
                    Category category = _context.Categories.FirstOrDefault(c => c.Id == deleteID);
                    if (category != null)
                    {
                        _context.Categories.Remove(category);
                        _context.SaveChanges();
                    }
                }
                else if (Constants.PRO_BRAND.Equals(property))
                {
                    Brand brand = _context.Brands.FirstOrDefault(c => c.Id == deleteID);
                    if (brand != null)
                    {
                        _context.Brands.Remove(brand);
                        _context.SaveChanges();
                    }
                }
                else if (Constants.PRO_SIZE.Equals(property))
                {
                    Size size = _context.Sizes.FirstOrDefault(c => c.Id == deleteID);
                    if (size != null)
                    {
                        _context.Sizes.Remove(size);
                        _context.SaveChanges();
                    }
                }
                else if (Constants.PRO_COLOR.Equals(property))
                {
                    Color color = _context.Colors.FirstOrDefault(c => c.Id == deleteID);
                    if (color != null)
                    {
                        _context.Colors.Remove(color);
                        _context.SaveChanges();
                    }
                }

                return Json(new { success = true });
            }
            
            return Json(new { success = false });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("UpdateProperty")]
        public IActionResult UpdateProperty(string property, int updateID, string propertyName)
        {
            bool updated = false;
            if (!String.IsNullOrEmpty(property) && updateID >= 0 && !String.IsNullOrEmpty(propertyName))
            {
                if (Constants.PRO_CATEGORY.Equals(property))
                {
                    Category category = _context.Categories.FirstOrDefault(c => c.Id == updateID);
                    if (category != null && !propertyName.Equals(category.Name))
                    {
                        category.Name = propertyName;
                        _context.SaveChanges();
                        updated = true;
                    }
                }
                else if (Constants.PRO_BRAND.Equals(property))
                {
                    Brand brand = _context.Brands.FirstOrDefault(c => c.Id == updateID);
                    if (brand != null && !propertyName.Equals(brand.Name))
                    {
                        brand.Name = propertyName;
                        _context.SaveChanges();
                        updated = true;
                    }
                }
                else if (Constants.PRO_SIZE.Equals(property))
                {
                    Size size = _context.Sizes.FirstOrDefault(c => c.Id == updateID);
                    int sizeVal = Convert.ToInt32(propertyName);
                    if (size != null && sizeVal != size.SizeVal)
                    {
                        size.SizeVal = sizeVal;
                        _context.SaveChanges();
                        updated = true;
                    }
                }
                else if (Constants.PRO_COLOR.Equals(property))
                {
                    Color color = _context.Colors.FirstOrDefault(c => c.Id == updateID);
                    if (color != null && !propertyName.Equals(color.Cname))
                    {
                        color.Cname = propertyName;
                        _context.SaveChanges();
                        updated = true;
                    }
                }

                return Json(new { success = true, updated = updated });
            }

            return Json(new { success = false });
        }

        public IActionResult SliderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);


            return View();
        }
    }
}
