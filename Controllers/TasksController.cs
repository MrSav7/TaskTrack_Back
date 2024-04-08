using KyrsachAPI.Entities.Tasks;
using KyrsachAPI.Models;
using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KyrsachAPI.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksService _taskService;

        public TasksController(ILogger<TasksController> logger, ITasksService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        private int userId => Convert.ToInt32(User.FindFirstValue("userId"));

        [HttpGet("GetAllBrands")]
        public List<Brands> GetAllBrands()
        {
            var result = _taskService.GetAllBrands();
            return result;
        }

        [HttpGet("GetAllProductTypes")]
        public List<ProductType> GetAllProductTypes()
        {
            var result = _taskService.GetAllProductTypes();
            return result;
        }

        [Authorize(Roles="admin,manager")]
        [HttpPost("CreateNewTask")]
        public IActionResult CreateNewTask([FromBody] CreateTask newtask)
        {
            var result = _taskService.CreateNewTask(newtask);
            return result ? Ok() : BadRequest("Произошла ошибка"); 
        }

        [HttpPost("GetMyTasks")]
        public IActionResult GetMyTasks([FromBody] string statuses)
        {
            var mytasks = _taskService.GetUserTasks(userId, statuses);
            return new JsonResult(mytasks);
        }

        [Authorize(Roles = "admin, manager")]
        [HttpPost("GetTasks")]
        public IActionResult GetNeedCall([FromBody] string statuses)
        {
            var tasks = _taskService.GetUserTasks(null, statuses);
            return new JsonResult(tasks);
        }

        [HttpGet("Task/{taskId}")]
        public TaskInfo GetTaskInfo(int taskId)
        {
            var tasks = _taskService.GetTaskInfo(taskId);
            return tasks;
        }

        [HttpPost("Task/{TaskId}/ChangeStatus")]
        public IActionResult ChangeStatus(int TaskId, [FromBody] int statusId)
        {
            _taskService.ChangeStatus(TaskId, statusId, userId);
            return Ok();
        }

        [HttpPost("Task/{TaskId}/AddStep")]
        public IActionResult AddStep(int TaskId, [FromBody] string comment)
        {
            _taskService.AddStep(TaskId, comment, userId);
            return Ok();
        }

        [HttpGet("Task/{TaskId}/GetSteps")]
        public IActionResult GetSteps(int TaskId)
        {
            var tasks = _taskService.GetSteps(TaskId);
            return new JsonResult(tasks);
        }

        [HttpGet("GetStatuses")]
        public List<TaskStatuses> GetStatuses()
        {
            return _taskService.GetStatuses();
        }

        [HttpPost("Task/{TaskId}/ChangePlanTime")]
        public IActionResult ChangePlanTime(int TaskId, [FromBody] DateTime date)
        {
            _taskService.ChangePlanTime(TaskId, date, userId);
            return Ok();
        }

    }
}
