
namespace SmartBin.Infrastructure.Domain.Models.Bin
{
    public class BinUnit
    {
        public string BinUnitId { get; set; }
        public string BinId { get; set; }
        public BinUnitType Type { get; set; }
        public Bin Bin {  get; set; }
        public List<CollectedHistory> CollectedHistories { get; set; }
        public List<ErrorHistory> ErrorHistories { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BinUnit() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BinUnit(string id, string binId, BinUnitType type, Bin bin, List<CollectedHistory> collectedHistories, List<ErrorHistory> errorHistories)
        {
            BinUnitId = id;
            BinId = binId;
            Type = type;
            Bin = bin;
            CollectedHistories = collectedHistories;
            ErrorHistories = errorHistories;
        }
    }
}
