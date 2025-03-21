
namespace SmartBin.Infrastructure.Domain.Resources.BinUnits
{
    public class BinUnitViewModel
    {
        public string BinUnitId { get; set; }
        public BinUnitType Type { get; set; }
        public List<CollectedHistoryViewModel> CollectedHistories { get; set; }
    }
}
