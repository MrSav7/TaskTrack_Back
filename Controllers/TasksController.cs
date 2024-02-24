using KyrsachAPI.Entities.Tasks;
using KyrsachAPI.Models;
using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAllBrands")]
        public IActionResult GetAllBrands()
        {
            var result = _taskService.GetAllBrands();
            return new JsonResult(result);
        }

        [HttpGet("GetAllProductTypes")]
        public IActionResult GetAllProductTypes()
        {
            var result = _taskService.GetAllProductTypes();
            return new JsonResult(result);
        }

        [Authorize(Roles="admin")]
        [HttpPost("CreateNewTask")]
        public IActionResult CreateNewTask([FromBody] CreateTask newtask)
        {
            var result = _taskService.CreateNewTask(newtask);
            return result ? Ok() : BadRequest("Произошла ошибка"); 
        }
    }
}
