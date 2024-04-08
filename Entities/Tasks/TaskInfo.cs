using user = KyrsachAPI.Models.User.User; 

namespace KyrsachAPI.Entities.Tasks
{
    public class TaskInfo: UsersTasks
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string? Executor { get; set; }
        public int? ExecId { get; set; }
    }
}
