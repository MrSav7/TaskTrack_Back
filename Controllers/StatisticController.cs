using KyrsachAPI.Entities.Statistic;
using KyrsachAPI.Entities.Tasks;
using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KyrsachAPI.Controllers
{
    [Authorize(Roles = "manager, admin")]
    [Route("[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly ILogger<StatisticController> _logger;
        private readonly IStatisticService _statisticService;

        public StatisticController(ILogger<StatisticController> logger, IStatisticService statisticService)
        {
            _logger = logger;
            _statisticService = statisticService;
        }

        [HttpGet("TaskStastus")]
        public IActionResult TaskStastusToday()
        {
            var result = _statisticService.TaskStastusToday();
            return new JsonResult(result);
        }

        [HttpPost("TaskStastusFiltered")]
        public IActionResult TaskStastusFiltered([FromBody] TaskStastusFilter dates)
        {
            var result = _statisticService.TaskStastusFiltered(dates);
            return new JsonResult(result);
        }

        [HttpGet("TaskStatusUsers")]
        public List<TaskStatusUser> TaskStatusUsers()
        {
            return _statisticService.TaskStatusUsers();
        }

        [HttpGet("TaskStatBrand")]
        public List<TaskStatBrand> TaskStatBrand()
        {
            return _statisticService.TaskStatBrand();
        }

        [HttpGet("TaskStatProdType")]
        public List<TaskStatProdType> TaskStatProdType()
        {
            return _statisticService.TaskStatProdType();
        }
    }
}
