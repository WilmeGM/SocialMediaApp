using SocialNetwork.Core.Application.Dtos.Friendship;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IFriendshipIdentityRepository : IGenericRepository<Friendship>
    {
        Task<List<Friend>> GetUserFriendsAsync(string userId);
        Task<Friendship> GetFriendshipAsync(string userId, string friendId);
    }
}
