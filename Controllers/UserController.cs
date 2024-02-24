using KyrsachAPI.Entities.User;
using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KyrsachAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(ILogger<UserController> logger, IUserService userService, ITokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return new JsonResult(_userService.GetAllUsers().Select(
               res => new {userId = res.UserId})) ;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] RequestLogin user)
        {
            if (string.IsNullOrEmpty(user.UserLogin) || string.IsNullOrEmpty(user.UserPassword) || user.UserPassword.Length <=3)
            {
                return BadRequest(new {message = "Неправильный логин или пароль"});
            }

            var result = _userService.Authenticate(user.UserLogin, user.UserPassword);
            if(result == null || result.UserActiveStatus == false)
            {
                return BadRequest(new { message = "Неправильный логин или пароль"});
            }

            var newToken = _tokenService.GenerateNewToken(result);

            HttpContext.Response.Cookies.Append("token", newToken.ToString());
            return Ok();
        }
    }
}
