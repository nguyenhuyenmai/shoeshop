using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.ViewModels;

namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý sản phẩm - product
    /// </summary>
    public class ProductService
    {
        private readonly DBContext _context;

        public ProductService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách toàn bộ product trong cửa hàng
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            List<Product> listProducts = new List<Product>();

            var result = _context.Products.ToList();
            if (result != null && result.Count > 0)
            {
                foreach (var product in result)
                {
                    listProducts.Add(product);
                }
            }

            return listProducts;
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get list all variant of product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ProductDetails> GetListProductVariant(int id)
        {
            List<ProductDetails> listProduct = new List<ProductDetails>();
            var result = (from pv in _context.ProductVariants
                          join p in _context.Products on pv.ProductId equals p.Id
                          join c in _context.Colors on pv.ColorId equals c.Id
                          join s in _context.Sizes on pv.SizeId equals s.Id
                          where pv.ProductId == id
                          orderby s.SizeVal, c.Cvalue
                          select new ProductDetails
                          {
                              pId = pv.Id,
                              colorID = c.Id,
                              colorVal = c.Cvalue,
                              colorName = c.Cname,
                              quantity = pv.Quantity,
                              sizeId = s.Id,
                              size = s.SizeVal
                          }).ToList();

            if (result != null && result.Count > 0)
            {
                foreach (var variant in result)
                {
                    listProduct.Add(variant);
                }
            }
            return listProduct;
        }
    }
}
