using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Slider
{
    public int SliderId { get; set; }

    public string Image { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Backlink { get; set; }

    public string Note { get; set; } = null!;

    public int Status { get; set; }
}
