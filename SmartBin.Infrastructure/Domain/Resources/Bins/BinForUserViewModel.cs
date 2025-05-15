
namespace SmartBin.Infrastructure.Domain.Resources.Bins
{
    public class BinForUserViewModel
    {
        public double Longtitude {  get; set; }
        public double Latitude { get; set; }
        public string Address {  get; set; }
        public int? Battery {  get; set; }
        public int? Internet { get; set; }
    }
}
