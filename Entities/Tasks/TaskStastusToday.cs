namespace KyrsachAPI.Entities.Tasks
{
    public class TaskStastusToday
    {
        public int NotClosed { get; set; }
        public int Created { get; set; }
        public int InWork { get; set; }
        public int Completed { get; set; }
        public int Closed { get; set; }
        public string timePeriod { get; set; }
    }
}
