namespace KyrsachAPI.Entities.Tasks
{
    public class CreateTask
    {
        /// <summary>
        /// Используется для создания новой задачи
        /// </summary>
        public int TaskItemBrandId { get; set; }

        public int TaskProductTypeId { get; set; }

        public string TaskProductModel { get; set; } = null!;

        public int TaskStatusId { get; } = 1;

        public string TaskUserProblemDesc { get; set; } = null!;
    }
}