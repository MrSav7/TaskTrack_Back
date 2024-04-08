using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KyrsachAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly ILogger<IdentityController> _logger;
        private readonly IIdentityService _identityService;

        //private int UserId => Int32.TryParse(User.Identity!.userId);
        private int UserId => Convert.ToInt32(User.FindFirstValue("userId"));
        //private int UserId = 1;
        public IdentityController(ILogger<IdentityController> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        [HttpGet("getMenuItems")]
        public IActionResult GetMenuItems() 
        {
            var result = _identityService.GetMenuItems(UserId);
            return new JsonResult(result);
        }
    }
}
