using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Shoe
{
    public int ShoeId { get; set; }

    public int ShoeLineId { get; set; }

    public string ShoeName { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public string ThumbnailPath { get; set; } = null!;

    public int AverageRating { get; set; }

    public float Discount { get; set; }

    public bool? IsFeature { get; set; }

    public bool? Status { get; set; }

    public DateTime? Created_Date { get; set; }

	public DateTime? Updated_Date { get; set; }

	public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ShoeImage> ShoeImages { get; set; } = new List<ShoeImage>();

    public virtual ShoeLine ShoeLine { get; set; } = null!;

    public virtual ICollection<ShoeVariant> ShoeVariants { get; set; } = new List<ShoeVariant>();
}
