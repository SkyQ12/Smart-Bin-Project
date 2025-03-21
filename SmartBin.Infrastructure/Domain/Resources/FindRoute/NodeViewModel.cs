
namespace SmartBin.Infrastructure.Domain.Resources.FindRoute
{
    public class NodeViewModel
    {
        public string Id { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public double Fullness { get; set; }  // Node fullness (0-100%)

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public NodeViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public NodeViewModel(string id, double longtitude, double latitude, double fullness)
        {
            Id = id;
            Longtitude = longtitude;
            Latitude = latitude;
            Fullness = fullness;
        }
    }
}
