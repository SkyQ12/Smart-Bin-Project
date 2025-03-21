
namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PointChangeController : ControllerBase
    {
        public IPointChangeService _pointChangeService { get; set; }

        public PointChangeController(IPointChangeService pointChangeService)
        {
            _pointChangeService = pointChangeService;
        }

        [HttpGet]
        [Route("GetPointChangedHistory")]
        public async Task<List<PointChangeHistoryViewModel>> GetPointChangedHistory([FromQuery] string userId)
        {
            return await _pointChangeService.GetPointChangedHistory(userId);
        }

        [HttpPatch]
        [Route("UpdatePoint")]
        public async Task<IActionResult> UpdatePoint([FromQuery] string id, [FromBody] UpdatePointViewModel point)
        {
            var update = await _pointChangeService.UpdatePoint(id, point);
            if (update)
            {
                return new OkObjectResult("Update points successfully!");
            }
            else
            {
                return new OkObjectResult($"Unable to update point: {id}");
            }
        }
    }
}
