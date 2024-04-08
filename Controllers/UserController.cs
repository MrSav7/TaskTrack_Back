using KyrsachAPI.Entities.User;
using KyrsachAPI.Models.User;
using KyrsachAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Common;

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

            //HttpContext.Response.Cookies.Append("token", newToken.ToString());
            return new JsonResult(new { token = newToken });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("AmdGetUsersById/{userId}")]
        public List<AmdGetUsers> AmdGetUsersById(int? userId) {
            return _userService.AmdGetUsers(userId);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("AmdGetUsers")]
        public List<AmdGetUsers> AmdGetUsers()
        {
            return _userService.AmdGetUsers(null);
        }

        [HttpGet("StatusTypes")]
        public List<UserActiveStatus> getStatusTipes()
        {
            return _userService.getStatusTipes();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("ChangeUserStatus")]
        public IActionResult ChangeUserStatus([FromBody] ChangeUserStatusFiler f)
        {
            var r = _userService.ChangeUserStatus(f.userId, f.statusId);
            if (r)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPost("CreateNewUser")]
        public IActionResult CreateNewUser([FromBody] NewUserForm form)
        {
            _userService.CreateNewUser(form);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetRoles")]
        public List<UserRole> GetRoles()
        {
            return _userService.GetRoles();
        }
    }
}
