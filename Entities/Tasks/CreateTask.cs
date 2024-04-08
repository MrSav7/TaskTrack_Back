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

        public string TaskCustomerFirstName { get; set; } = null!;

        public string TaskCustomerLastName { get; set; } = null!;

        public string TaskCustomerMiddleName { get; set; }

        public string TaskCustomerPhone { get; set; } = null!;

        public int ExecutorId { get; set; }
    }
}