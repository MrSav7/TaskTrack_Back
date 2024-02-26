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

        [HttpGet]
        public IActionResult TaskStastusToday()
        {
            var result = _statisticService.TaskStastusToday();
            return new JsonResult(result);
        }
    }
}
