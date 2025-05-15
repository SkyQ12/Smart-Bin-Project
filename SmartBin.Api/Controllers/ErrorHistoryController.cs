using SmartBin.Infrastructure.Services.ErrorHistories;

namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorHistoryController:ControllerBase
    {

        private readonly IErrorHistoryService _errorHistoryService;

        public ErrorHistoryController(IErrorHistoryService errorHistoryService)
        {
            _errorHistoryService = errorHistoryService ?? throw new ArgumentNullException(nameof(errorHistoryService));
        }

        [HttpGet]
        [Route("GetErrorHistoryByBinUnitId")]
        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoryByBinUnitId([FromQuery] string binUnitId)
        {
            return await _errorHistoryService.GetErrorHistoryByBinUnitId(binUnitId);
        }

        [HttpGet]
        [Route("GetErrorHistoriesByDateTime")]
        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoriesByDateTime([FromQuery] DateTime timeStamp)
        {
            return await _errorHistoryService.GetErrorHistoriesByDateTime(timeStamp);
        }

        [HttpGet]
        [Route("GetErrorHistoriesFromDateTime1ToDateTime2")]
        public async Task<List<ErrorHistoryViewModel>> GetErrorHistoriesFromDateTime1ToDateTime2([FromQuery] DateTime timeStamp1, [FromQuery] DateTime timeStamp2)
        {
            return await _errorHistoryService.GetErrorHistoriesFromDateTime1ToDateTime2(timeStamp1, timeStamp2);
        }

        [HttpDelete]
        [Route("DeleteErrorHistoriesByBinUnitId")]
        public async Task<bool> DeleteErrorHistoriesByBinUnitId([FromQuery] string binUnitId)
        {
            return await _errorHistoryService.DeleteErrorHistoriesByBinUnitId(binUnitId);
        }

        [HttpDelete]
        [Route("DeleteErrorHistoriesFromDateTime1ToDateTime2")]
        public async Task<bool> DeleteErrorHistoriesFromDateTime1ToDateTime2([FromQuery] DateTime timeStamp1, [FromQuery] DateTime timeStamp2)
        {
            return await _errorHistoryService.DeleteErrorHistoriesFromDateTime1ToDateTime2(timeStamp1, timeStamp2);
        }
    }
}
