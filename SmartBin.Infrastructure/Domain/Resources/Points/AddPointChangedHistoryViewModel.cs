
namespace SmartBin.Infrastructure.Domain.Resources.Points
{
    public class AddPointChangedHistoryViewModel
    {
        public string UserId { get; set; }
        public int OldPoint { get; set; }
        public int NewPoint { get; set; }
        public DateTime PointChangedTime { get; set; }

        public AddPointChangedHistoryViewModel(string userId, int oldPoint, int currentPoint, DateTime pointChangedTime)
        {
            UserId = userId;
            OldPoint = oldPoint;
            NewPoint = currentPoint;
            PointChangedTime = pointChangedTime;
        }
    }
}
