using System.ComponentModel.DataAnnotations;

namespace KyrsachAPI.Models
{
    public class TaskStatuses
    {
        [Key]
        public int TaskStatusId { get; set; }
        public string TaskStatusName { get; set; }
    }
}
