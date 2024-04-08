using KyrsachAPI.Models.User;
using static System.Net.Mime.MediaTypeNames;

namespace KyrsachAPI.Entities.User
{
    public class AmdGetUsers
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserMiddleName { get; set; }
        public DateTime UserCreateDate { get; set; }
        public string UserEmail { get; set; }
        public bool UserActiveStatus { get; set; }
        public string UserActStName { get; set; }
        public int UserRoleId { get; set; }
        public string RoleTextName { get; set; }
        public int taskNotClosed { get; set; }
    }
}
