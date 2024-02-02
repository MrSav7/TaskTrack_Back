using KyrsachAPI.Context;

namespace KyrsachAPI.Service
{
    public interface IUserService
    {

    }

    public class UserService: IUserService
    {
        private readonly TaskTrackContext _context;

        public UserService(TaskTrackContext context)
        {
            _context = context;
        }
    }
}
