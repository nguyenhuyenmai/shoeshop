using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class ShoeLine
{
    public int ShoeLineId { get; set; }

    public int BrandId { get; set; }

    public string ShoeLineName { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
