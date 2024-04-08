using KyrsachAPI.Context;
using KyrsachAPI.Entities.Statistic;
using KyrsachAPI.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoredProcedureEFCore;

namespace KyrsachAPI.Service
{
    public interface IStatisticService
    {
        List<TaskStastusToday> TaskStastusToday();
        List<TaskStatusUser> TaskStatusUsers();
        TaskStastusToday TaskStastusFiltered(TaskStastusFilter dates);
        List<TaskStatBrand> TaskStatBrand();
        List<TaskStatProdType> TaskStatProdType();
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

        public List<TaskStastusToday> TaskStastusToday()
        {
            List<TaskStastusToday> b = new List<TaskStastusToday>();
            _taskTrackContext.LoadStoredProc("[dbo].[TaskStastus]")
                .Exec(x => b = x.ToList<TaskStastusToday>());
            return b;
        }


        public List<TaskStatusUser> TaskStatusUsers()
        {
            List<TaskStatusUser> res = new List<TaskStatusUser>();
            _taskTrackContext.LoadStoredProc("[dbo].[TaskStatusUsers]")
                .Exec(x => res = x.ToList<TaskStatusUser>());
            return res;
        }
        public TaskStastusToday TaskStastusFiltered(TaskStastusFilter dates)
        {
            TaskStastusToday b = new TaskStastusToday();
            _taskTrackContext.LoadStoredProc("[dbo].[TaskStastusFiltered]")
                .AddParam("dateStart", dates.dateStart)
                .AddParam("dateEnd", dates.dateEnd)
                .Exec(x => b = x.FirstOrDefault<TaskStastusToday>());
            return b;
        }

        public List<TaskStatBrand> TaskStatBrand()
        {
            List<TaskStatBrand> r = new List<TaskStatBrand>();

            _taskTrackContext.LoadStoredProc("[dbo].[TaskStatBrands]")
                .Exec(data =>  r = data.ToList<TaskStatBrand>());
            return r;
        }
        
        public List<TaskStatProdType> TaskStatProdType()
        {
            List<TaskStatProdType> r = new List<TaskStatProdType>();

            _taskTrackContext.LoadStoredProc("[dbo].[TaskStatProdTypes]")
                .Exec(data => r = data.ToList<TaskStatProdType>());
            return r;
        }
    }
}
