
namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminService _adminService {  get; set; }

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("RegisterNewUserAdmin")]
        public async Task<IActionResult> RegisterNewUserAdmin([FromBody] RegisterUserAdminViewModel registerUserAdmin)
        {
            var isSucess = await _adminService.RegisterNewUserAdmin(registerUserAdmin);
            if (isSucess)
            {
                return new OkObjectResult("Registered successfully.");
            }
            else
            {
                return new OkObjectResult("Username already exists.");
            }
        }
        [HttpPost]
        [Route("RegisterNewWorkerAdmin")]
        public async Task<IActionResult> RegisterNewWorkerAdmin([FromBody] RegisterWorkerAdminViewModel registerUserAdmin)
        {
            var isSucess = await _adminService.RegisterNewWorkerAdmin(registerUserAdmin);
            if (isSucess)
            {
                return new OkObjectResult("Registered successfully.");
            }
            else
            {
                return new OkObjectResult("Username already exists!");
            }
        }
        [HttpPost]
        [Route("RegisterNewBinAdmin")]
        public async Task<IActionResult> RegisterNewBinAdmin([FromBody] RegisterBinAdminViewModel registerUserAdmin)
        {
            var isSucess = await _adminService.RegisterNewBinAdmin(registerUserAdmin);
            if (isSucess)
            {
                return new OkObjectResult("Registered successfully.");
            }
            else
            {
                return new OkObjectResult("Username already exists!");
            }
        }
        [HttpGet]
        [Route("GetUserAdmin")]
        public async Task<List<AdminViewModel>> GetUserAdmin()
        {
            return await _adminService.GetUserAdmin();
        }
        [HttpGet]
        [Route("GetWorkerAdmin")]
        public async Task<List<AdminViewModel>> GetWorkerAdmin()
        {
            return await _adminService.GetWorkerAdmin();
        }
        [HttpGet]
        [Route("GetBinAdmin")]
        public async Task<List<AdminViewModel>> GetBinAdmin()
        {
            return await _adminService.GetBinAdmin();
        }
    }
}
