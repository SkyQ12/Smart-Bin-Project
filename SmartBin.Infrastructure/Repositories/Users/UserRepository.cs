
namespace SmartBin.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.Where(x => x.Role == "User").ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string UserId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId && x.Role == "User");
            return user != null ? user : throw new ResourceNotfoundException("Not found user!");
        }

        public async Task<User> GetUserByUserNameAsync(string UserName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == UserName && x.Role == "User");
            return user != null ? user : throw new ResourceNotfoundException("Not found user!");
        }

        public async Task<bool> IsExistUser(string element)
        {
            var isexist = await _context.Users.AnyAsync(x => x.UserName == element || x.Id == element);
            return isexist;
        }

        public async Task<User> RegisterNewUserAsync(User user)
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
            return random.Next(10000000,99999999).ToString();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserInfoAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(LoginViewModel user)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName ==  user.UserName && x.Password == user.Password);
            return currentUser != null ? currentUser : throw new ResourceNotfoundException("UserName or Password is incorrect!");
        }
    }
}
