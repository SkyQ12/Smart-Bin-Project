using SmartBin.Infrastructure.Services.CollectedHistory;
namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollectedHistoriesController : ControllerBase
    {
        public ICollectedHistoryService _collectedHistoryService { get; set; }

        public CollectedHistoriesController(ICollectedHistoryService collectedHistoryService)
        {
            _collectedHistoryService = collectedHistoryService;
        }

        [HttpPost]
        [Route("CreateCollectedHistory")]
        public async Task<IActionResult> CreateCollectedHistory([FromBody] AddCollectedHistoryViewModel collectedHistoryViewModel)
        {
            var isSuccess = await _collectedHistoryService.CreateCollectedHistory(collectedHistoryViewModel);
            if (isSuccess)
            {
                return new OkObjectResult("Create collected history successfully!");
            }
            else
            {
                return new BadRequestObjectResult("Create collected history failed!");
            }
        }
        [HttpDelete]
        [Route("DeleteCollectedHistoriesByBinUnitId")]
        public async Task<IActionResult> DeleteCollectedHistoriesByBinUnitId(string binUnitId)
        {
            var isSuccess = await _collectedHistoryService.DeleteCollectedHistoriesByBinUnitId(binUnitId);
            if (isSuccess)
            {
                return new OkObjectResult("Delete collected histories by bin unit id successfully!");
            }
            else
            {
                return new BadRequestObjectResult("Delete collected histories by bin unit id failed!");
            }
        }
        [HttpDelete]
        [Route("DeleteCollectedHistoriesFromDateTime1ToDateTime2")]
        public async Task<IActionResult> DeleteCollectedHistoriesFromDateTime1ToDateTime2(DateTime collectedTime1, DateTime collectedTime2)
        {
            var isSuccess = await _collectedHistoryService.DeleteCollectedHistoriesFromDateTime1ToDateTime2(collectedTime1, collectedTime2);
            if (isSuccess)
            {
                return new OkObjectResult("Delete collected histories from date time 1 to date time 2 successfully!");
            }
            else
            {
                return new BadRequestObjectResult("Delete collected histories from date time 1 to date time 2 failed!");
            }
        }
        [HttpDelete]
        [Route("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            var isSuccess = await _collectedHistoryService.DeleteAll();
            if (isSuccess)
            {
                return new OkObjectResult("Delete all collected histories successfully!");
            }
            else
            {
                return new BadRequestObjectResult("Delete all collected histories failed!");
            }
        }
     
    }
}
