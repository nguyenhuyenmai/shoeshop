using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ShoeVariantId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ShoeVariant ShoeVariant { get; set; } = null!;
}
