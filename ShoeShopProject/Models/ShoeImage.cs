using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class ShoeImage
{
    public int Id { get; set; }

    public int ShoeId { get; set; }

    public string Image { get; set; } = null!;

    public virtual Shoe Shoe { get; set; } = null!;
}
