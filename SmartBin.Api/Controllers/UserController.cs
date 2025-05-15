
using Newtonsoft.Json.Linq;
using SmartBin.Infrastructure.Domain.Models.Person;

namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<UserViewModel>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<UserViewModel> GetUserById([FromQuery] string userId)
        {
            return await _userService.GetUserById(userId);
        }
        [HttpGet]
        [Route("GetUserByUserName")]
        public async Task<UserViewModel> GetUserByUserName([FromQuery] string userName)
        {
            return await _userService.GetUserByUserName(userName);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser([FromBody] CreateNewUserViewModel createNew)
        {
            var newUser = await _userService.RegisterNewUser(createNew);
            if (newUser)
            {
                return new OkObjectResult("Registered successfully.");
            }
            else
            {
                return new OkObjectResult("Username already exists.");
            }
        }
        [HttpDelete]
        [Route("DeleteUserById")]
        public async Task<IActionResult> DeleteUserById([FromQuery] string userId)
        {
            var delete = await _userService.DeleteUserById(userId);
            return new OkObjectResult("User deleted successfully.");
        }
        [HttpPatch]
        [Route("UpdateUserInfomation")]
        public async Task<IActionResult> UpdateUserInfomation([FromQuery] string userName,  [FromBody] UpdateUserInfoViewModel updateUserInfo)
        {
            var isSuccess = await _userService.UpdateUserInfo(userName, updateUserInfo);
            if (isSuccess)
            {
                return new OkObjectResult("Updated user infomation successfully.");
            }
            else
            {
                return new OkObjectResult("User not found!");
            }
        }
        [HttpPatch]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromQuery] string Id, [FromBody] PasswordChangeViewModel changePassword)
        {
            var result = await _userService.ChangePassword(Id, changePassword);
            return new OkObjectResult(result);
        }

        [HttpPatch]
        [Route("ChangePasswordByUserName")]
        public async Task<IActionResult> ChangePasswordByUserName([FromQuery] string userName, [FromBody] PasswordChangeViewModel changePassword)
        {
            var result = await _userService.ChangePasswordByUserName(userName, changePassword);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var token = await _userService.Login(login);
            return new OkObjectResult(token);
        }
    }
}
