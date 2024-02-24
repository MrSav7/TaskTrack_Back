using KyrsachAPI.Context;
using KyrsachAPI.Entities.Tasks;
using KyrsachAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace KyrsachAPI.Service
{
    public interface ITasksService
    {
        List<Brands> GetAllBrands();
        List<ProductType> GetAllProductTypes();
        bool CreateNewTask(CreateTask newtask);
    }

    public class TasksService: ITasksService
    {
        private readonly ILogger<TasksService> _logger;
        private readonly TaskTrackContext _trackContext;

        public TasksService(ILogger<TasksService> logger, TaskTrackContext trackContext)
        {
            _logger = logger;
            _trackContext = trackContext;
        }

        public List<Brands> GetAllBrands() {
            return _trackContext.Brands
                .AsNoTracking()
                .OrderBy(o => o.BrandName)
                .ToList();
        }

        public List<ProductType> GetAllProductTypes()
        {
            return _trackContext.ProductTypes
                .AsNoTracking()
                .OrderBy(o => o.ProductTypeName)
                .ToList();
        }

        public bool CreateNewTask(CreateTask newtask)
        {
            
            using (var tr = _trackContext.Database.BeginTransaction())
            {
                var t = new Tasks()
                {
                    TaskItemBrandId = newtask.TaskItemBrandId,
                    TaskProductTypeId = newtask.TaskProductTypeId,
                    TaskProductModel = newtask.TaskProductModel,
                    TaskStatusId = newtask.TaskStatusId,
                    TaskUserProblemDesc = newtask.TaskUserProblemDesc
                };

                _trackContext.Tasks.Add(t);
                _trackContext.SaveChanges();

                /*int? taskId = _trackContext.Tasks.AsNoTracking()
                    .Where(o => o.TaskStatusId == newtask.TaskStatusId && o.TaskUserProblemDesc == newtask.TaskUserProblemDesc)
                    .FirstOrDefault()
                    .TaskId;*/

                /*if(taskId == null)
                {
                    tr.Rollback();
                    _logger.LogInformation("Не удалось создать шаг к заявке");
                    return false;
                }*/

                _trackContext.TaskSteps.Add(new TaskStep()
                {
                    TaskId = t.TaskId,
                    TaskStepText = $"Задача создана. Проблема заявителя: {newtask.TaskUserProblemDesc}",
                    TaskStepUserId = null
                });
                _trackContext.SaveChanges();
                tr.Commit();
            }

            return true;
        }
    }
}
