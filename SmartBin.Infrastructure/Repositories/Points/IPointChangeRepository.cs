
namespace SmartBin.Infrastructure.Repositories.Points
{
    public interface IPointChangeRepository
    {
        public Task UpdatePointAsync(User user);
        public Task AddPointChangedHistory(PointChangedHistory history);
        public Task<List<PointChangedHistory>> GetPointsChangedHistoryAsync(string userId);
    }
}
