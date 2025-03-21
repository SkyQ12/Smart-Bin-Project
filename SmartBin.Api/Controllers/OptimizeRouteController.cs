
namespace SmartBin.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OptimizeRouteController : ControllerBase
    {
        public IOptimizeRouteService _optimizeRouteService { get; set; }

        public OptimizeRouteController(IOptimizeRouteService optimizeRouteService)
        {
            _optimizeRouteService = optimizeRouteService;
        }

        [HttpPost]
        [Route("FindBestRoute")]
        public async Task<IActionResult> FindBestRoute([FromBody] OptimizeRouteRequest request)
        {
            if (request.Nodes == null || request.Nodes.Count == 0)
                return BadRequest("Danh sách các node không được để trống.");

            var optimizedRoute = await _optimizeRouteService.OptimizeRoutes(request.Nodes ,request.StartingPoint.Latitude, request.StartingPoint.Longitude);
            return new OkObjectResult(optimizedRoute);
        }
    }
    public class OptimizeRouteRequest
    {
        public Coordinate StartingPoint { get; set; }
        public List<NodeViewModel> Nodes { get; set; }
    }
}
