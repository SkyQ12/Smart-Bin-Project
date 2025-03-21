
namespace SmartBin.Infrastructure.Domain.Resources.CollectedHistories
{
    public class AddErrorHistoryViewModel
    {
        public string BinUnitId {  get; set; }
        public string ErrorName {  get; set; }
        public DateTime TimeStamp {  get; set; }

        public AddErrorHistoryViewModel(string binUnitId, string errorName, DateTime timeStamp)
        {
            BinUnitId = binUnitId;
            ErrorName = errorName;
            TimeStamp = timeStamp;
        }
    }
}
