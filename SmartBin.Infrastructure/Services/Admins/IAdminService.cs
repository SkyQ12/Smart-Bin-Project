
namespace SmartBin.Infrastructure.Services.Admins
{
    public interface IAdminService
    {
        public Task<bool> RegisterNewUserAdmin(RegisterUserAdminViewModel viewModel);
        public Task<bool> RegisterNewWorkerAdmin(RegisterWorkerAdminViewModel viewModel);
        public Task<bool> RegisterNewBinAdmin(RegisterBinAdminViewModel viewModel);
        public Task<List<AdminViewModel>> GetUserAdmin();
        public Task<List<AdminViewModel>> GetWorkerAdmin();
        public Task<List<AdminViewModel>> GetBinAdmin();
    }
}
