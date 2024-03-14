using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int ShoeVariantId { get; set; }

    public int Quantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ShoeVariant ShoeVariant { get; set; } = null!;
}
