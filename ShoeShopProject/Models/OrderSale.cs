using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class OrderSale
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int Sales { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User SalesNavigation { get; set; } = null!;
}
