
namespace SmartBin.Infrastructure.Repositories.Admins
{
    public interface IAdminRepository
    {
        public Task<bool> IsExistAdmin(string property);
        public Task<User> RegisterNewAdminAsync(User user);
        public Task<List<User>> GetUserAdmin();
        public Task<List<User>> GetWorkerAdmin();
        public Task<List<User>> GetBinAdmin();
    }
}
