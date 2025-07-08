using SocialNetwork.Core.Application.Dtos.Friendship;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ResetPasswordAsync(ForgotPasswordRequest request);
        Task UpdateUserProfilePictureAsync(string userId, string profilePictureUrl);
        Task<SaveUserViewModel> GetUserByIdAsync(string userId);
        Task UpdateUserProfileAsync(SaveUserViewModel vm);
        Task<Friend?> GetFriendByUsernameAsync(string username);
        Task<bool> IsEmailRegistered(string email, string username);
    }
}