using System;
using System.Collections.Generic;

namespace KyrsachAPI.Models;

public partial class MenuItemsAccess
{
    public int AccessMenuId { get; set; }

    public int AccessMenuRoleId { get; set; }

    public int? AccessMenuItemId { get; set; }
}
