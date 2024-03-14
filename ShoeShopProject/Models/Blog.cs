using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Brief { get; set; } = null!;

    public string Desciption { get; set; } = null!;

    public int Author { get; set; }

    public string Image { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool IsFearture { get; set; }

    public bool Status { get; set; }

    public virtual User AuthorNavigation { get; set; } = null!;

    public virtual BlogCategory Category { get; set; } = null!;
}
