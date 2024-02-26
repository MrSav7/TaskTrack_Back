using KyrsachAPI.Context;
using KyrsachAPI.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoredProcedureEFCore;

namespace KyrsachAPI.Service
{
    public interface IStatisticService
    {
        TaskStastusToday TaskStastusToday();
    }

    public class StatisticService: IStatisticService
    {
        private readonly TaskTrackContext _taskTrackContext;
        private readonly ILogger<StatisticService> _logger;

        public StatisticService(TaskTrackContext taskTrackContext, ILogger<StatisticService> logger)
        {
            _taskTrackContext = taskTrackContext;
            _logger = logger;
        }

        public TaskStastusToday TaskStastusToday()
        {
            TaskStastusToday b = new TaskStastusToday();
            _taskTrackContext.LoadStoredProc("[dbo].[TaskStastusToday]")
                .Exec(x => b = x.FirstOrDefault<TaskStastusToday>());
            return b;
        }
    }
}
