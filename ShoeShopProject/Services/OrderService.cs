﻿using ShoeShopProject.Data;
using ShoeShopProject.Models;
using ShoeShopProject.ViewModels;
using System.Net;
namespace ShoeShopProject.Services
{
    /// <summary>
    /// Serive quản lý order
    /// </summary>
    public class OrderService
    {
        private readonly DBContext _context;

        public OrderService(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cart checkout
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="userID"></param>
        /// <param name="totalAmount"></param>
        /// <param name="userName"></param>
        /// <param name="phone"></param>
        /// <param name="address"></param>
        /// <param name="orderNote"></param>
        /// <param name="paymentMethod"></param>
        /// <returns></returns>
        public int CartCheckout(Cart cart, int userID, int totalAmount, string userName, string phone, string address, string orderNote, int paymentMethod)
        {
            int orderID = -1;
            if (cart != null)
            {
                // Add order
                Order order = new Order
                {
                    UserId = userID,
                    OrderAddress = address,
                    OrderPhone = phone,
                    OrderName = userName,
                    PaymentMethod = paymentMethod,
                    OrderStatus = Constants.NOT_APPROVE_ORDER,
                    PaymentStatus = Constants.NOT_PAYMENT_ORDER,
                    OrderDesc = orderNote,
                    TotalAmount = Convert.ToDecimal(totalAmount),
                    UpdateDate = DateTime.Now
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                List<CartItem> listCartItem = _context.CartItems.Where(c => c.CartId == cart.Id).ToList();
                if (listCartItem!= null && listCartItem.Count > 0)
                {
                    foreach (CartItem item in listCartItem)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            AmountPrice = item.PriceAmount
                        };
                        _context.OrderDetails.Add(orderDetail);
                        _context.CartItems.Remove(item);
                        _context.SaveChanges();
                    }
                }
                orderID = order.Id;
            }
            
            return orderID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
		public List<OrderItemDetails> GetListOrderItemDetails(int orderID)
        {
			List<OrderItemDetails> details = new List<OrderItemDetails>();
            var result = (from od in _context.OrderDetails
                          join o in _context.Orders on od.OrderId equals o.Id
                          join p in _context.ProductVariants on od.ProductId equals p.Id
                          where od.OrderId == orderID
                          select new OrderItemDetails
                          {
                              Id = od.OrderId,
                              productName = p.Product.Name,
                              colorName = p.Color.Cname,
                              size = p.Size.SizeVal,
                              productId = p.ProductId,
                              quantity = od.Quantity,
                              priceAmout = od.AmountPrice,
                          }).ToList();

            if (result != null && result.Count > 0)
            {
                details.AddRange(result);
            }

			return details;
		}
	}
}
