namespace SmartBin.Infrastructure.Domain.Resources.BinUnits
{
    public class BinUnitViewModel
    {
        public string BinUnitId { get; set; }
        public BinUnitType Type { get; set; }
        public int Fault { get; set; }
        public int Level { get; set; }
        public int CompressCnt { get; set; }
        public int FullCnt { get; set; }
        public int Status { get; set; }
        public int Flame { get; set; }
        public int Vibration { get; set; }
        public List<CollectedHistoryViewModel> CollectedHistories { get; set; }
    }
}