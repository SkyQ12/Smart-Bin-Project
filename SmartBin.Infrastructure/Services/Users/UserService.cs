
namespace SmartBin.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }
        public IUnitOfWork _unitOfWork {  get; set; }
        public IMapper _mapper { get; set; }
        private readonly JwtSetting _jwtSetting;
public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtSetting> jwtSetting)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSetting = jwtSetting.Value;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var source = await _userRepository.GetAllUserAsync();
            var result = _mapper.Map<List<User>, List<UserViewModel>>(source);
            return result;
        }

        public async Task<UserViewModel> GetUserById(string UserId)
        {
            var source = await _userRepository.GetUserByIdAsync(UserId);
            var result = _mapper.Map<User, UserViewModel>(source);
            return result;
        }

        public async Task<UserViewModel> GetUserByUserName(string UserName)
        {
            var source = await _userRepository.GetUserByUserNameAsync(UserName);
            var result = _mapper.Map<User, UserViewModel>(source);
            return result;
        }

        public async Task<bool> RegisterNewUser(CreateNewUserViewModel userViewModel)
        {
            var isexist = await _userRepository.IsExistUser(userViewModel.UserName);
            if (isexist)
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<CreateNewUserViewModel, User>(userViewModel);
                var userEntry = await _userRepository.RegisterNewUserAsync(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> DeleteUserById(string UserId)
        {
            var isexist = await _userRepository.GetUserByIdAsync(UserId);
            if (isexist is not null) 
            {
                await _userRepository.DeleteUser(isexist);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserInfo(string userId, UpdateUserInfoViewModel updateViewModel)
        {
            var isExist = await _userRepository.IsExistUser(userId);
            if (isExist)
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (!string.IsNullOrEmpty(updateViewModel.Name)) 
                {
                    user.Name = updateViewModel.Name;
                }
                if (!string.IsNullOrEmpty(updateViewModel.UserName))
                {
                    user.UserName = updateViewModel.UserName;
                }
                if (!string.IsNullOrEmpty(updateViewModel.IdentificationNumber))
                {
                    user.IdentificationNumber = updateViewModel.IdentificationNumber;
                }
                if (!string.IsNullOrEmpty(updateViewModel.Sex))
                {
                    user.Sex = updateViewModel.Sex;
                }
                if (!string.IsNullOrEmpty(updateViewModel.Birthday.ToString()))
                {
                    user.Birthday = updateViewModel.Birthday;
                }
                if (!string.IsNullOrEmpty(updateViewModel.HomeTown))
                {
                    user.HomeTown = updateViewModel.HomeTown;
                }
                if (!string.IsNullOrEmpty(updateViewModel.IssuanceDate.ToString()))
                {
                    user.IssuanceDate = updateViewModel.IssuanceDate;
                }

                await _userRepository.UpdateUserInfoAsync(user);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<string> ChangePassword(string Id, PasswordChangeViewModel viewModel)
        {
            var isExist = await _userRepository.IsExistUser(Id);
            if (isExist)
            {
                var user = await _userRepository.GetUserByIdAsync(Id);
                if(user.Password != viewModel.CurrentPassword)
                {
                    return "Current password is incorrect";
                }
                else if (string.IsNullOrEmpty(viewModel.NewPassword)) 
                {
                    return "The new password cannot be left blank"!;
                }
                else if(viewModel.CurrentPassword == viewModel.NewPassword)
                {
                    return "The new password cannot be the same as the current password";
                }
                else
                {
                    user.Password = viewModel.NewPassword;
                    await _userRepository.UpdateUserInfoAsync(user);
                    await _unitOfWork.CompleteAsync();
                    return "Change password successfully!";
                }
            }
            else
            {
                return "Not found user with this Id";
            }
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var user = await _userRepository.LoginAsync(loginViewModel);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myClaims = new[]
            {
                new Claim(ClaimTypes.SerialNumber, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                claims: myClaims, 
                expires: DateTime.UtcNow.AddMinutes(30), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
