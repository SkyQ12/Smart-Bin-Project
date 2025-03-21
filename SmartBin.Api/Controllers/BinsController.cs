
namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BinsController : ControllerBase
    {
        public IBinService _binService { get; set; }

        public BinsController(IBinService binService)
        {
            _binService = binService;
        }
        [HttpGet]
        [Route("GetAllBins")]
        public async Task<List<BinViewModel>> GetAllBins()
        {
            return await _binService.GetAllBins();
        }
        [HttpGet]
        [Route("GetBinById")]
        public async Task<BinViewModel> GetBinById([FromQuery] string binId)
        {
            return await _binService.GetBinById(binId);
        }
        [HttpGet]
        [Route("GetBinsForUser")]
        public async Task<List<BinForUserViewModel>> GetBinsForUser()
        {
            return await _binService.GetBinsForUser();
        }
        [HttpDelete]
        [Route("DeleteBinById")]
        public async Task<IActionResult> DeleteBinById([FromQuery] string binId)
        {
            await _binService.DeleteBinById(binId);
            return new OkObjectResult("Deleted bin successfully!");
        }
        [HttpPost]
        [Route("CreateNewBin")]
        public async Task<IActionResult> CreateNewBin([FromBody] AddNewBinViewModel viewModel)
        {
            var isSuccess = await _binService.CreateNewBin(viewModel);
            if (isSuccess)
            {
                return new OkObjectResult("Create new bin successfully!");
            }
            else
            {
                return new OkObjectResult("This bin id already exist!");
            }
        }
        [HttpPatch]
        [Route("UpdateBinById")]
        public async Task<IActionResult> UpdateBinById([FromQuery] string id, [FromBody] UpdateBinViewModel viewModel)
        {
            var isSuccess = await _binService.UpdateBin(id, viewModel);
            if (isSuccess)
            {
                return new OkObjectResult("Update bin successfull!");
            }
            else
            {
                return new OkObjectResult("Bin can not be found with this id!");
            }
        }
    }
}
