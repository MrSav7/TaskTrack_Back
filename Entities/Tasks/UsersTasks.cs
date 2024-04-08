using KyrsachAPI.Models;

namespace KyrsachAPI.Entities.Tasks
{
    public class UsersTasks
    {
        public int TaskId { get; set; }

        public string BrandName { get; set; }

        public string ProductTypeName { get; set; }

        public string TaskProductModel { get; set; } = null!;

        public DateTime? TaskCreateDate { get; set; }

        public string TaskStatusName { get; set; }

        public DateTime? TaskPlanExeTime { get; set; }

        public DateTime? TaskCloseDate { get; set; }

        public string TaskUserProblemDesc { get; set; } = null!;

        public string? TaskProblems { get; set; }

        //public string? Executors { get; set; }
    }
}
