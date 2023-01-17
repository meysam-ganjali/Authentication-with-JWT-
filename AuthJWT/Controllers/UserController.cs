using AuthJWT.Model;
using AuthJWT.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthJWT.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {


        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
       
        public ActionResult Login([FromBody] UserLogin loginParam) {
            var user = _userService.Authentication(loginParam.UserName, loginParam.Password);
            if (user == null)
            {
                _logger.LogError($"User {loginParam.UserName} Not Found");
                return NotFound();
            } else
            {
                _logger.LogInformation($"Login {loginParam.UserName} is Successed");
                return Ok(user);
            }
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Create Request Get All User");
            return Ok(_userService.GetAll());
        }
    }
}