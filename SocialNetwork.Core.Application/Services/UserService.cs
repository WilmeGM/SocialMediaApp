using AutoMapper;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            return await _accountService.AuthenticateAsync(loginRequest);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterUserAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token) => await _accountService.ConfirmAccountAsync(userId, token);

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm)
        {
            ForgotPasswordRequest forgotPasswordRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(forgotPasswordRequest);
        }

        public async Task UpdateUserProfilePictureAsync(string userId, string profilePictureUrl)
        {
            await _accountService.UpdateUserProfilePictureAsync(userId, profilePictureUrl);
        }

        public async Task<SaveUserViewModel> GetSaveUserViewModelByIdAsync(string userId)
        {
            return await _accountService.GetUserByIdAsync(userId);
        }

        public async Task UpdateUserProfileAsync(SaveUserViewModel vm)
        {
            var isEmailRegistered = await _accountService.IsEmailRegistered(vm.Email, vm.UserName);

            if (isEmailRegistered)
            {
                throw new Exception("Email is already taken");
            }
            
            await _accountService.UpdateUserProfileAsync(vm);
        }

    }
}
