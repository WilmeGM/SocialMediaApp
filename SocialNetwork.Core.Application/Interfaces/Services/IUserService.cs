using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task SignOutAsync();
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task UpdateUserProfilePictureAsync(string userId, string profilePictureUrl);
        Task<SaveUserViewModel> GetSaveUserViewModelByIdAsync(string userId);
        Task UpdateUserProfileAsync(SaveUserViewModel vm);
    }
}
