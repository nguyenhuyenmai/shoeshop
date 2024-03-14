using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ShoeId { get; set; }

    public int RatedStar { get; set; }

    public string? FeedbackContent { get; set; }

    public bool Status { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Shoe Shoe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
