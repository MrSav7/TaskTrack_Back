using KyrsachAPI.Models.User;
using static System.Net.Mime.MediaTypeNames;

namespace KyrsachAPI.Entities.User
{
    public class NewUserForm
    {
        public string UserLogin {  get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string? UserMiddleName { get; set; }
        public string UserEmail { get; set; }
        public int UserRoleId { get; set; }
    }
}
