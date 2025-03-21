namespace SmartBin.Infrastructure.Domain.Models.Bin
{
    public class Bin
    {
        public string Id { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public List<BinUnit> BinUnits { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Bin() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Bin(string id, double longtitude, double latitude, string address, List<BinUnit> binUnits)
        {
            Id = id;
            Longtitude = longtitude;
            Latitude = latitude;
            Address = address;
            BinUnits = binUnits;
        }
    }
}
