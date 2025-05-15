
namespace SmartBin.Infrastructure.Domain.Models.Histories
{
    public class ErrorHistory
    {
        public int Id { get; set; }
        public string BinUnitId {  get; set; }
        public int ErrorId {  get; set; }
        public DateTime TimeStamp { get; set; }
        public BinUnit BinUnit { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ErrorHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public ErrorHistory(int id, string binUnitId, int errorName, DateTime timeStamp, BinUnit binUnit)
        {
            Id = id;
            BinUnitId = binUnitId;
            ErrorId = errorName;
            TimeStamp = timeStamp;
            BinUnit = binUnit;
        }
    }
}
