
namespace SmartBin.Infrastructure.Domain.Resources.CollectedHistories
{
    public class AddCollectedHistoryViewModel
    {
        public string BinUnitId {  get; set; }
        public DateTime CollectedTime {  get; set; }

        public AddCollectedHistoryViewModel(string binUnitId, DateTime collectedTime)
        {
            BinUnitId = binUnitId;
            CollectedTime = collectedTime;
        }
    }
}
