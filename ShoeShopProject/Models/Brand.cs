using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<ShoeLine> ShoeLines { get; set; } = new List<ShoeLine>();
}
