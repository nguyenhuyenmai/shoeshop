using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class BlogCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
