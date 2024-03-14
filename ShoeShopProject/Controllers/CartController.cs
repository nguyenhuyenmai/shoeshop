using Microsoft.AspNetCore.Mvc;
using ShoeShop.Data;
using ShoeShop.Models;
using ShoeShop.ViewModels;
using ShoeShop.Service;
using ShoeShop.Statics;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ShoeShop.Controllers
{
	[Route("Cart")]
	public class CartController : Controller
	{
		private readonly ILogger<CartController> _logger; // ho tro ghi log
		private readonly DataContext _context; // call data when loading controller
		private readonly MailService _mailService;

		public CartController(ILogger<HomeController> logger, DataContext context, MailService mailService)
		{
			_logger = logger;
			_context = context;
			_mailService = mailService;
		}

		[Route("AddToCart")]
		public IActionResult AddToCart(int shoeVariantId, int quantity)
		{

			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			// add product to cart
			EmrSession emrSession = new EmrSession(HttpContext);
			int userID = emrSession.userId;

			if (shoeVariantId >= 0)
			{
				CartService cartService = new CartService(_context);
				cartService.AddProductToCart(shoeVariantId, quantity, userID);

				return Json(new { success = true });
			}

			return Json(new { success = false });
		}

		[Route("CartDetails")]
		public IActionResult CartDetails(int userID)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			CartService cartService = new CartService(_context);
			int cartID = cartService.GetUserCartID(userID);
			ArrayList listCartItem = new ArrayList();
			if (cartID >= 0)
			{
				//view list product in Cart
				listCartItem = cartService.GetListProductCart(cartID);
			}

			// Kiểm tra nếu cart có product
			if (listCartItem != null && listCartItem.Count > 0)
			{
				ViewBag.CartID = cartID;
				ViewBag.ListCartItem = listCartItem;
			}
			else
			{
				ViewBag.CartID = -1;
			}



			return View();
		}

		[Route("DeleteCartItem")]
		public IActionResult DeleteCartItem(int cartId, int shoeVariantId)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			CartService cartService = new CartService(_context);
			// Nếu tồn tại cart item
			int cartItemID = cartService.CheckCartItemExist(cartId, shoeVariantId);
			if (cartItemID > -1)
			{
				cartService.DeleteCartItem(cartItemID, shoeVariantId);

				return Json(new { success = true });
			}

			return Json(new { success = false });
		}

			[Route("UpdateQuantityValue")]
		public IActionResult UpdateQuantityValue(int cartId, int shoeId, int quantity)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			CartService cartService = new CartService(_context);
			// Nếu tồn tại cart item
			int cartItemID = cartService.CheckCartItemExist(cartId, shoeId);
			if (cartItemID > -1)
			{
				int checksuccess = cartService.UpdateCartQuantity(cartItemID, quantity);
				if (checksuccess > -1)
				{
					return Json(new { success = true });
				}
			}

			return Json(new { success = false });
		}

		[Route("CartCheckout")]
		public IActionResult CartCheckout(int cartID)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			UserService userService = new UserService(_context);
			EmrSession emrSession = new EmrSession(HttpContext);
			ViewBag.User = userService.getUserProfile(emrSession.userId);

			CartService cartService = new CartService(_context);
			ArrayList listCartItem = new ArrayList();
			if (cartID >= 0)
			{
				//view list product in Cart
				listCartItem = cartService.GetListProductCart(cartID);
			}

			// Kiểm tra nếu cart có product
			if (listCartItem != null && listCartItem.Count > 0)
			{
				ViewBag.CartID = cartID;
				ViewBag.ListCartItem = listCartItem;
			}
			else
			{
				ViewBag.CartID = -1;
			}

			return View();
		}

		[Route("CartCompletion")]
		public IActionResult CartCompletion(int cartID, int userID)
		{
			string inputString = Request.Form["totalAmount"];
			decimal totalAmount;

			if (Decimal.TryParse(inputString, out totalAmount))
			{
				totalAmount = Decimal.Round(totalAmount, 2);
			}
			else
			{
				totalAmount = 0;
			}

			string orderAddress = Request.Form["address"];
			string userName = Request.Form["userName"];
			string userPhone = Request.Form["phone"];

			CartService cartService = new CartService(_context);
			UserService userService = new UserService(_context);
			ArrayList listCartItem = new ArrayList();
			if (cartID >= 0)
			{
				//list product in Cart
				listCartItem = cartService.GetListProductCart(cartID);
			}

			OrderService orderService = new OrderService(_context);
			int orderID = orderService.AddNewOrder(userID, totalAmount, orderAddress, DateTime.Now, StaticData.PROCESSING_STATUS, userID, DateTime.Now);

			if (orderID >= 0)
			{
				if (listCartItem != null && listCartItem.Count > 0)
				{
					foreach (CartDetailsModel item in listCartItem)
					{
						orderService.AddOrderItem(orderID, item.shoeVariantId, item.quantity, item.totalPrice);
						
					}
				}

				cartService.RemoveCartItem(cartID);
				userService.UpdateUsrFromOrder(userID, userName, userPhone, orderAddress);

				string[] bankAccount = _mailService.GetAccountBank();
				string emailContent = $"Cảm ơn bạn đã mua hàng của chúng tôi. Bạn đã đặt thành công đơn hàng có mã: #{orderID}.Vui lòng kiểm tra đơn hàng tại: \"{Url.Action("OrderCompletion", "Cart", new { orderID = orderID }, protocol: Request.Scheme)}\".Thực hiện chuyển khoản thanh toán qua số tài khoản: {bankAccount[0]} - {bankAccount[1]}";
				_mailService.SendToEmail(userService.getUserProfile(userID).Email, "Đặt hàng thành công", emailContent);

				return Json(new { success = true, orderID = orderID });
			}

			return Json(new { success = false });
		}

		[Route("OrderCompletion")]
		public IActionResult OrderCompletion(int orderID)
		{
			RouteMapping mapping = new RouteMapping();
			mapping.AddService(HttpContext, this, _context);

			OrderService orderService = new OrderService(_context);
			UserService userService = new UserService(_context);
			EmrSession emrSession = new EmrSession(HttpContext);

			Order order = orderService.GetOrderByID(orderID);
			ArrayList listOrderItem = orderService.GetListOrderItem(orderID);
			User customer = userService.getUserProfile(emrSession.userId);

			if (order != null)
			{

				ViewBag.Order = order;
				ViewBag.ListOrderItem = listOrderItem;
				ViewBag.Customer = customer;
			}

			return View();
		}
	}
}
