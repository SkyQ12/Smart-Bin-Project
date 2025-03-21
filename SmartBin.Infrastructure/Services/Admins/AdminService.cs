

namespace SmartBin.Infrastructure.Services.Admins
{
    public class AdminService : IAdminService
    {
        public IAdminRepository _adminRepository {  get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }

        public AdminService(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> RegisterNewUserAdmin(RegisterUserAdminViewModel viewModel)
        {
            var isexist = await _adminRepository.IsExistAdmin(viewModel.UserName);
            if (isexist)
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<RegisterUserAdminViewModel, User>(viewModel);
                var userEntry = await _adminRepository.RegisterNewAdminAsync(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> RegisterNewWorkerAdmin(RegisterWorkerAdminViewModel viewModel)
        {
            var isexist = await _adminRepository.IsExistAdmin(viewModel.UserName);
            if (isexist)
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<RegisterWorkerAdminViewModel, User>(viewModel);
                var userEntry = await _adminRepository.RegisterNewAdminAsync(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> RegisterNewBinAdmin(RegisterBinAdminViewModel viewModel)
        {
            var isexist = await _adminRepository.IsExistAdmin(viewModel.UserName);
            if (isexist)
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<RegisterBinAdminViewModel, User>(viewModel);
                var userEntry = await _adminRepository.RegisterNewAdminAsync(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<AdminViewModel>> GetUserAdmin()
        {
            var source = await _adminRepository.GetUserAdmin();
            var result = _mapper.Map<List<User>, List<AdminViewModel>>(source);
            return result;
        }

        public async Task<List<AdminViewModel>> GetWorkerAdmin()
        {
            var source = await _adminRepository.GetWorkerAdmin();
            var result = _mapper.Map<List<User>, List<AdminViewModel>>(source);
            return result;
        }

        public async Task<List<AdminViewModel>> GetBinAdmin()
        {
            var source = await _adminRepository.GetBinAdmin();
            var result = _mapper.Map<List<User>, List<AdminViewModel>>(source);
            return result;
        }
    }
}
