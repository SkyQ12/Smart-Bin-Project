
namespace SmartBin.Infrastructure.Domain.Models.Histories
{
    public class CollectedHistory
    {
        public int Id { get; set; }
        public string? BinId { get; set; }
        public string? BinUnitId { get; set; }
        public string? BinType {  get; set; }  
        public string Street { get; set; }
        public string District { get; set; }
        public DateTime CollectedTime { get; set; }
        public BinUnit BinUnit { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CollectedHistory() { }

        public CollectedHistory(int id, string binId,string? binUnitId, string? binType, string street, string district, DateTime collectedTime, BinUnit binUnit)
        {
            Id = id;
            BinId = binId;
            BinUnitId = binUnitId;
            BinType = binType;
            Street = street;
            District = district;
            CollectedTime = collectedTime;
            BinUnit = binUnit;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
