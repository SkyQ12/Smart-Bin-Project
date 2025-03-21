
namespace SmartBin.Infrastructure.Services.Point
{
    public interface IPointChangeService
    {
        public Task<List<PointChangeHistoryViewModel>> GetPointChangedHistory(string userId);
        public Task<bool> UpdatePoint(string id, UpdatePointViewModel newPoint);
    }
}
