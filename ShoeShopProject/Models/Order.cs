using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public string OrderAddress { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public int Status { get; set; }

    public DateTime UpdateDate { get; set; }

    public int UpdateUser { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderSale> OrderSales { get; set; } = new List<OrderSale>();

    public virtual User User { get; set; } = null!;
}
