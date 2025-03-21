
namespace SmartBin.Infrastructure.Services.Routes
{
    public interface IOptimizeRouteService
    {
        public Task<string> OptimizeRoutes(List<NodeViewModel> nodes, double startLatitude, double startLongitude);
    }
}
