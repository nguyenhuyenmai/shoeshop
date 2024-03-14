using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class CustomerStatus
{
    public int UserId { get; set; }

    public int? Status { get; set; }

    public virtual User User { get; set; } = null!;
}
