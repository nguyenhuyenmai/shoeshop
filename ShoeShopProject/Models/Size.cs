using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Size
{
    public int SizeId { get; set; }

    public float SizeValue { get; set; }

    public virtual ICollection<ShoeVariant> ShoeVariants { get; set; } = new List<ShoeVariant>();
}
