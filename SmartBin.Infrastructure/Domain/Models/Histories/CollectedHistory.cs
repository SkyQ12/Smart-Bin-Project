
namespace SmartBin.Infrastructure.Domain.Models.Histories
{
    public class CollectedHistory
    {
        public int Id { get; set; }
        public string BinUnitId {  get; set; }  
        public DateTime CollectedTime { get; set; }
        public BinUnit BinUnit { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CollectedHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CollectedHistory(int id, string binUnitId, DateTime collectedTime, BinUnit binUnit)
        {
            Id = id;
            BinUnitId = binUnitId;
            CollectedTime = collectedTime;
            BinUnit = binUnit;
        }
    }
}
