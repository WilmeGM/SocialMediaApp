using SocialNetwork.Core.Application.ViewModels.Friendship;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendshipService 
    {
        Task<List<FriendViewModel>> GetUserFriendsAsync(string userId);
        Task<string> AddFriendAsync(string userId, string friendUsername);
        Task RemoveFriendAsync(string userId, string friendId);
    }
}
