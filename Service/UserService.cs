using KyrsachAPI.Context;
using KyrsachAPI.Models.User;


namespace KyrsachAPI.Service
{
    public interface IUserService
    {
        List<User> GetAllUsers();
    }

    public class UserService: IUserService
    {
        private readonly TaskTrackContext _context;

        public UserService(TaskTrackContext context)
        {
            _context = context;
        }

        

        public List<User> GetAllUsers() {
            return _context.Users.ToList();
        }
    }
}
