namespace SmartBin.Infrastructure.Services.Routes
{
    public class OptimizeRouteService : IOptimizeRouteService
    {
        private readonly double _radius = 2.0; // Bán kính thu gom (km)

        public async Task<string> OptimizeRoutes(List<NodeViewModel> nodes, double startLatitude, double startLongitude)
        {
            string routestring = String.Empty;

            nodes = nodes.Where(node => node != null).ToList();
            // Tìm các node cần thu gom
            var nodesToCollect = nodes.Where(node => node.Fullness == 100).ToList();

            foreach (var node in nodesToCollect.ToList())
            {
                var nearbyNodes = nodes
                    .Where(n => n.Id != node.Id && n.Fullness >= 70 && CalculateDistance(node.Latitude, node.Longtitude, n.Latitude, n.Longtitude) <= _radius)
                    .ToList();
                nodesToCollect.AddRange(nearbyNodes);
            }

            nodesToCollect = nodesToCollect.Distinct().ToList();

            // Thêm điểm bắt đầu vào danh sách node
            var startingPoint = new NodeViewModel
            {
                Id = "0", // Giả định ID 0 cho điểm bắt đầu
                Latitude = startLatitude,
                Longtitude = startLongitude,
                Fullness = 0 // Không có fullness cho điểm bắt đầu
            };

            nodesToCollect.Insert(0, startingPoint);

            // Tìm lộ trình tối ưu
            var optimalRoute = FindOptimalRoute(nodesToCollect);

            routestring = string.Join(" -> ", optimalRoute.Select(node => node.Id));

            return routestring;
        }

        private List<NodeViewModel> FindOptimalRoute(List<NodeViewModel> nodes)
        {
            var ga = new GeneticAlgorithm(nodes);
            return ga.FindBestRoute();
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Bán kính Trái Đất (km)
            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (lon2 - lon1) * Math.PI / 180;
            double lat1Rad = lat1 * Math.PI / 180;
            double lat2Rad = lat2 * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1Rad) * Math.Cos(lat2Rad);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
    }
}
