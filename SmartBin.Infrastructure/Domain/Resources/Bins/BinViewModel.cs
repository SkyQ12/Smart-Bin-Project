
namespace SmartBin.Infrastructure.Domain.Resources.Bins
{
    public class BinViewModel
    {
        public string Id {  get; set; }
        public double Longtitude {  get; set; }
        public double Latitude { get; set; }
        public string Address {  get; set; }
        public int Battery {  get; set; }
        public int Internet { get; set; }
        public string Qr { get; set; }
        public List<BinUnitViewModel> BinUnits { get; set; }
    }
}
