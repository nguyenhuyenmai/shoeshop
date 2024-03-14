using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Color
{
    public int ColorId { get; set; }

    public string ColorName { get; set; } = null!;

    public virtual ICollection<ShoeVariant> ShoeVariants { get; set; } = new List<ShoeVariant>();
}
