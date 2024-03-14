using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class ShoeVariant
{
    public int ShoeVariantId { get; set; }

    public int ShoeId { get; set; }

    public int ColorId { get; set; }

    public int SizeId { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Color Color { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Shoe Shoe { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
