
namespace SmartBin.Infrastructure.Domain.Resources.CollectedHistories
{
    public class CollectedHistoryViewModel
    {
        public string BinId { get; set; }
        public string? BinType { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public DateTime CollectedTime { get; set; }
    }
}
