
namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BinUnitController : ControllerBase
    {
        public IBinUnitService _binUnitService {  get; set; }

        public BinUnitController(IBinUnitService binUnitService)
        {
            _binUnitService = binUnitService;
        }

        [HttpGet]
        [Route("GetBinUnitById")]
        public async Task<BinUnitViewModel> GetBinUnitById([FromQuery] string binUnitId) 
        {
            return await _binUnitService.GetBinUnitById(binUnitId);
        }
    }
}
