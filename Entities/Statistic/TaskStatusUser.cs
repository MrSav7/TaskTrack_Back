using System.Collections.Generic;

namespace KyrsachAPI.Entities.Statistic
{
    public class TaskStatusUser
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int Total { get; set; }
        public int NotCloset { get; set; }
        public int Closet { get; set; }
    }
}
