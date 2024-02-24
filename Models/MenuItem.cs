using System;
using System.Collections.Generic;

namespace KyrsachAPI;

public partial class MenuItem
{
    public int MenuItemId { get; set; }

    public string MenuItemName { get; set; } = null!;

    public string? MenuItemIcon { get; set; }

    public int MenuItemOrderNumber { get; set; }

    public string? MenuItemsRoute { get; set; }
}
