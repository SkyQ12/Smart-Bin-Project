
namespace SmartBin.Infrastructure.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUserAsync();
        public Task<User> GetUserByIdAsync(string UserId);
        public Task<User> GetUserByUserNameAsync(string UserName);
        public Task<bool> IsExistUser(string element);
        public Task<User> RegisterNewUserAsync(User user);
        public Task DeleteUser(User user);
        public Task UpdateUserInfoAsync(User user);
        public Task<User> LoginAsync(LoginViewModel user);
    }
}
