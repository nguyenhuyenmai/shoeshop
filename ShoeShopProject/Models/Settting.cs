using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Settting
{
    public int Id { get; set; }

    public string SettingCode { get; set; } = null!;

    public string SettingType { get; set; } = null!;

    public string SettingName { get; set; } = null!;

    public string? Desciption { get; set; }

    public int UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }
}
