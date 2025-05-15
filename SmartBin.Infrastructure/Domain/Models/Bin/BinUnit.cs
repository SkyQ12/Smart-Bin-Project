
namespace SmartBin.Infrastructure.Domain.Models.Bin
{
    public class BinUnit
    {
        public string BinUnitId { get; set; }
        public string BinId { get; set; }
        public BinUnitType Type { get; set; }
        public Bin Bin {  get; set; }
        public int Fault { get; set; }
        public int Level { get; set; }
        public int CompressCnt { get; set; }
        public int FullCnt { get; set; }
        public int Status { get; set; }
        public int Flame { get; set; }
        public int Vibration { get; set; }
        public List<CollectedHistory> CollectedHistories { get; set; }
        public List<ErrorHistory> ErrorHistories { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BinUnit() { }

        public BinUnit(string binUnitId, string binId, BinUnitType type, Bin bin, int fault, int level, int compressCnt, int fullCnt, int status, int flame, int vibration, List<ErrorHistory> errorHistories, List<CollectedHistory> collectedHistories)
        {
            BinUnitId = binUnitId;
            BinId = binId;
            Type = type;
            Bin = bin;
            Fault = fault;
            Level = level;
            CompressCnt = compressCnt;
            FullCnt = fullCnt;
            Status = status;
            Flame = flame;
            Vibration = vibration;
            //CollectedHistories = collectedHistories;
            ErrorHistories = errorHistories;
            CollectedHistories = collectedHistories;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
