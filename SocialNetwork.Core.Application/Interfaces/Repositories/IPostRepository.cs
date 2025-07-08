using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsWithCommentsAsync();
        Task<List<Post>> GetFriendsPostsAsync(string userId, List<string> friendsIds);
    }
}
