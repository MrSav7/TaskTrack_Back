namespace KyrsachAPI.Entities.Tasks
{
    public class TaskStastusToday
    {
        public int NotClosed { get; set; }
        public int TodayCreated { get; set; }
        public int InWork { get; set; }
        public int ClosedToday { get; set; }
    }
}
