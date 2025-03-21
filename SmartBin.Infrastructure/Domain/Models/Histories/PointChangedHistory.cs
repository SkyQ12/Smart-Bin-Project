
namespace SmartBin.Infrastructure.Domain.Models.Histories
{
    public class PointChangedHistory
    {
        public int Id { get; set; }
        public string UserId {  get; set; }
        public int OldPoint {  get; set; }
        public int NewPoint { get; set; }
        public DateTime PointChangedTime { get; set; }
        public User User { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PointChangedHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public PointChangedHistory(int id, string userId, int oldPoint, int newPoint, DateTime pointChangedTime, User user)
        {
            Id = id;
            UserId = userId;
            OldPoint = oldPoint;
            NewPoint = newPoint;
            PointChangedTime = pointChangedTime;
            User = user;
        }
    }
}
