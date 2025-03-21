

namespace SmartBin.Infrastructure.Repositories.Admins
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsExistAdmin(string property)
        {
            var isexist = await _context.Users.AnyAsync(x => x.UserName == property || x.Id == property);
            return isexist;
        }

        public async Task<User> RegisterNewAdminAsync(User user)
        {
            bool isexist;
            do
            {
                user.Id = GenerateUserId();
                isexist = await _context.Users.AnyAsync(x => x.Id == user.Id);
            }
            while (isexist);
            var userEntry = await _context.Users.AddAsync(user);
            return userEntry.Entity;
        }
        public string GenerateUserId()
        {
            var random = new Random();
            return random.Next(10000000, 99999999).ToString();
        }

        public async Task<List<User>> GetUserAdmin()
        {
            return await _context.Users.Where(x => x.Role == "UserAdmin").ToListAsync();
        }

        public async Task<List<User>> GetWorkerAdmin()
        {
            return await _context.Users.Where(x => x.Role == "WorkerAdmin").ToListAsync();
        }

        public async Task<List<User>> GetBinAdmin()
        {
            return await _context.Users.Where(x => x.Role == "BinAdmin").ToListAsync();
        }
    }
}
