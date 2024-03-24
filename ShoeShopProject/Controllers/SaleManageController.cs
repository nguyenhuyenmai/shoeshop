using Microsoft.AspNetCore.Mvc;
using ShoeShopProject.Data;
using ShoeShopProject.Models;

namespace ShoeShopProject.Controllers
{
    public class SaleManageController : Controller
    {
        private readonly ILogger<SaleManageController> _logger;
        private readonly DBContext _context;

        public SaleManageController(ILogger<SaleManageController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult OrderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            return View();
        }

        public IActionResult MeOrderManageList()
        {
            ServiceMapping serviceMapping = new ServiceMapping(_context, HttpContext);
            serviceMapping.MappingHeaderAdmin(this);

            return View();
        }


    }
}
