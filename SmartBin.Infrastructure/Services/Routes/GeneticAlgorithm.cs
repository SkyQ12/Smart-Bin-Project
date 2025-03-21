
namespace SmartBin.Infrastructure.Services.Routes
{
    public class GeneticAlgorithm
    {
        private readonly List<NodeViewModel> _nodes;
        private readonly Random _random = new Random();
        private readonly int _populationSize = 100; // Kích thước quần thể
        private readonly int _generations = 200;   // Số thế hệ
        private readonly double _mutationRate = 0.05; // Tỷ lệ đột biến
        private readonly double _eliteRate = 0.1;     // Tỷ lệ cá thể ưu tú

        public GeneticAlgorithm(List<NodeViewModel> nodes)
        {
            _nodes = nodes;
        }

        public List<NodeViewModel> FindBestRoute()
        {
            var population = InitializePopulation();
            for (int generation = 0; generation < _generations; generation++)
            {
                population = EvolvePopulation(population);
            }
            return population.OrderBy(route => CalculateRouteDistance(route)).First();
        }

        private List<List<NodeViewModel>> InitializePopulation()
        {
            var population = new List<List<NodeViewModel>>();
            for (int i = 0; i < _populationSize; i++)
            {
                // Bắt đầu từ điểm 0 và xáo trộn các node còn lại
                var route = new List<NodeViewModel> { _nodes[0] }; // Điểm bắt đầu luôn cố định
                route.AddRange(_nodes.Skip(1).OrderBy(_ => _random.Next()));
                population.Add(route);
            }
            return population;
        }

        private List<List<NodeViewModel>> EvolvePopulation(List<List<NodeViewModel>> population)
        {
            var newPopulation = new List<List<NodeViewModel>>();
            int eliteCount = (int)(_eliteRate * _populationSize);
            var elites = population.OrderBy(route => CalculateRouteDistance(route)).Take(eliteCount).ToList();
            newPopulation.AddRange(elites);

            while (newPopulation.Count < _populationSize)
            {
                var parent1 = TournamentSelection(population);
                var parent2 = TournamentSelection(population);
                var child = OrderCrossover(parent1, parent2);

                if (_random.NextDouble() < _mutationRate)
                {
                    ThreeOptMutation(child);
                }
                newPopulation.Add(child);
            }
            return newPopulation;
        }

        private List<NodeViewModel> TournamentSelection(List<List<NodeViewModel>> population)
        {
            int tournamentSize = 5;
            var tournament = new List<List<NodeViewModel>>();
            for (int i = 0; i < tournamentSize; i++)
            {
                tournament.Add(population[_random.Next(population.Count)]);
            }
            return tournament.OrderBy(route => CalculateRouteDistance(route)).First();
        }

        private List<NodeViewModel> OrderCrossover(List<NodeViewModel> parent1, List<NodeViewModel> parent2)
        {
            int start = _random.Next(1, parent1.Count); // Không thay đổi điểm đầu tiên
            int end = _random.Next(start, parent1.Count);

            var child = Enumerable.Repeat((NodeViewModel)null, parent1.Count).ToList();
            for (int i = start; i <= end; i++)
            {
                child[i] = parent1[i];
            }

            int childIndex = (end + 1) % child.Count;
            foreach (var gene in parent2.Skip(1)) // Bỏ qua điểm bắt đầu
            {
                if (!child.Contains(gene))
                {
                    child[childIndex] = gene;
                    childIndex = (childIndex + 1) % child.Count;
                }
            }

            // Điểm bắt đầu luôn cố định
            child[0] = parent1[0];
            return child;
        }

        private void ThreeOptMutation(List<NodeViewModel> route)
        {
            int size = route.Count;
            int i = _random.Next(1, size); // Không thay đổi điểm đầu tiên
            int j = (i + 1) % size;
            int k = (j + 1) % size;

            var temp = route[i];
            route[i] = route[j];
            route[j] = route[k];
            route[k] = temp;
        }

        private double CalculateRouteDistance(List<NodeViewModel> route)
        {
            double distance = 0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                distance += CalculateDistance(route[i], route[i + 1]);
            }
            return distance;
        }

        private double CalculateDistance(NodeViewModel a, NodeViewModel b)
        {
            double R = 6371; // Bán kính Trái Đất (km)
            double dLat = (b.Latitude - a.Latitude) * Math.PI / 180;
            double dLon = (b.Longtitude - a.Longtitude) * Math.PI / 180;
            double lat1Rad = a.Latitude * Math.PI / 180;
            double lat2Rad = b.Latitude * Math.PI / 180;

            double aVal = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                          Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1Rad) * Math.Cos(lat2Rad);
            double c = 2 * Math.Atan2(Math.Sqrt(aVal), Math.Sqrt(1 - aVal));
            return R * c;
        }
    }
}
