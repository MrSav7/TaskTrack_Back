using System;
using System.Collections.Generic;

namespace KyrsachAPI.Models.User;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleTextName { get; set; } = null!;

    public bool RoleIsDefault { get; set; }
}
