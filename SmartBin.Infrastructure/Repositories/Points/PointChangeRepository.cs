
namespace SmartBin.Infrastructure.Repositories.Points
{
    public class PointChangeRepository : BaseRepository, IPointChangeRepository
    {
        public PointChangeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddPointChangedHistory(PointChangedHistory history)
        {
            _context.PointChangedHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PointChangedHistory>> GetPointsChangedHistoryAsync(string userId)
        {
            return await _context.PointChangedHistories.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task UpdatePointAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
