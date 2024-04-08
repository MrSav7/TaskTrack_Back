using KyrsachAPI.Context;
using KyrsachAPI.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace KyrsachAPI.Service
{
    public interface IIdentityService
    {
        List<MenuItem> GetMenuItems(int userId);
        int GetUserRoleId(int userId);
    }

    public class IdentityService: IIdentityService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly TaskTrackContext _trackContext;

        public IdentityService(ILogger<IdentityService> logger, TaskTrackContext trackContext)
        {
            _logger = logger;
            _trackContext = trackContext;
        }

        public int GetUserRoleId(int userId)
        {
            return _trackContext.Users.Where(u => u.UserId == userId)
                .FirstOrDefault()
                .UserId;
        }

        public List<MenuItem> GetMenuItems(int userId)
        {
            int roleId = GetUserRoleId(userId);
            return _trackContext.MenuItems
                .OrderBy(o => o.MenuItemOrderNumber)
                .Join(_trackContext.MenuItemsAccesses.Where(a => a.AccessMenuRoleId == roleId),
                m => m.MenuItemId,
                ma => ma.AccessMenuItemId,
                (m, ma) => new MenuItem()
                {
                    MenuItemId = m.MenuItemId,
                    MenuItemName = m.MenuItemName,
                    MenuItemOrderNumber = m.MenuItemOrderNumber,
                    MenuItemsRoute = m.MenuItemsRoute
                } )
                .ToList();
        }
    }
}
