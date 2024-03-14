using System;
using System.Collections.Generic;

namespace ShoeShop.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public int RoleId { get; set; }

    public string? Permisson { get; set; }

    public virtual Role Role { get; set; } = null!;
}
