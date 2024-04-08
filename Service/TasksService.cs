using KyrsachAPI.Context;
using KyrsachAPI.Entities.Tasks;
using KyrsachAPI.Models;
using KyrsachAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using StoredProcedureEFCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KyrsachAPI.Service
{
    public interface ITasksService
    {
        List<Brands> GetAllBrands();
        List<ProductType> GetAllProductTypes();
        bool CreateNewTask(CreateTask newtask);
        List<UsersTasks> GetUserTasks(int? userId, string statuses);
        List<UsersTasks> GetNeedCall(string statuses);
        void ChangeStatus(int TaskId, int statusId, int userId);
        void AddStep(int TaskId, string comment, int userId);
        List<TaskStep> GetSteps(int TaskId);
        TaskInfo GetTaskInfo(int taskId);
        List<TaskStatuses> GetStatuses();
        void ChangePlanTime(int TaskId, DateTime date, int userId);
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

                _trackContext.TaskSteps.Add(new TaskStep()
                {
                    TaskId = t.TaskId,
                    TaskStepText = $"Задача создана. Проблема заявителя: {newtask.TaskUserProblemDesc}",
                    TaskStepUserId = null
                });

                _trackContext.TaskCustomer.Add(new TaskCustomer()
                {
                    TaskId= t.TaskId,
                    TaskCustomerFirstName = newtask.TaskCustomerFirstName,
                    TaskCustomerLastName = newtask.TaskCustomerLastName,
                    TaskCustomerMiddleName = newtask.TaskCustomerMiddleName,
                    TaskCustomerPhone = newtask.TaskCustomerPhone,
                });

                _trackContext.TaskExecutors.Add(new TaskExecutor()
                {
                    TaskId = t.TaskId,
                    UserId = newtask.ExecutorId
                });

                _trackContext.SaveChanges();
                tr.Commit();
            }

            return true;
        }

        public List<UsersTasks> GetUserTasks(int? userId, string statuses)
        {
            var tasks = new List<UsersTasks>();
            _trackContext.LoadStoredProc("[dbo].[GetUserTasks]")
                .AddParam("userId", userId)
                .AddParam("taskStatus", statuses)
                .Exec(result => tasks = result.ToList<UsersTasks>());
            return tasks;
        }

        public List<UsersTasks> GetNeedCall(string statuses)
        {
            var tasks = new List<UsersTasks>();
            _trackContext.LoadStoredProc("[dbo].[GetUserTasks]")
                .AddParam("taskStatus", statuses)
                .Exec(result => tasks = result.ToList<UsersTasks>());
            return tasks;
        }

        public void ChangeStatus(int TaskId, int statusId, int userId)
        {
            var task = _trackContext.Tasks.Where(t => t.TaskId == TaskId)
                .FirstOrDefault();
            task.TaskStatusId = statusId;

            string statusName = _trackContext.TaskStatuses.Where(t => t.TaskStatusId == statusId)
                .AsNoTracking()
                .FirstOrDefault()
                .TaskStatusName;

            var user = _trackContext.Users.Where(u => u.UserId == userId)
                .AsNoTracking()
                .Select(u => u.UserFirstName + " " + u.UserLastName)
                .FirstOrDefault();

            _trackContext.TaskSteps.Add(new TaskStep()
            {
                TaskId = TaskId,
                TaskStepText = $"Пользователь {user} сменил(а) статус заявки на: {statusName}"
            });
            _trackContext.SaveChanges();
        }

        public void AddStep(int TaskId, string comment, int userId)
        {
            _trackContext.TaskSteps.Add(new TaskStep
            {
                TaskId = TaskId,
                TaskStepUserId = userId,
                TaskStepText = comment
            });
            _trackContext.SaveChanges();
        }

        public List<TaskStep> GetSteps(int TaskId)
        {
            var result = _trackContext.TaskSteps.Where(t => t.TaskId == TaskId)
                .OrderBy(c => c.TaskStepCreateDate)
                .AsNoTracking()
                .ToList();
            return result;
        }

        public TaskInfo GetTaskInfo(int taskId)
        {
            var task = new TaskInfo();
            _trackContext.LoadStoredProc("[dbo].[GetTaskById]")
                .AddParam("taskId", taskId)
                .Exec(result => task = result.FirstOrDefault<TaskInfo>()) ;
            return task;
        }

        public List<TaskStatuses> GetStatuses()
        {
            return _trackContext.TaskStatuses.ToList();
        }

        public void ChangePlanTime(int TaskId, DateTime date, int userId)
        {
            var task = _trackContext.Tasks.Where(t => t.TaskId == TaskId).First();
            var taskStep = new TaskStep()
            {
                TaskId = TaskId,
                TaskStepText = $"Пользователь {userId} сменил(а) планируемую дату закрытия заявки на {Convert.ToDateTime(date)}"
            };
            using (var tr = _trackContext.Database.BeginTransaction())
            {
                task.TaskPlanExeTime = Convert.ToDateTime(date);
                _trackContext.TaskSteps.Add(taskStep);
                _trackContext.SaveChanges();
                tr.Commit();
            }

        }
    }
}
