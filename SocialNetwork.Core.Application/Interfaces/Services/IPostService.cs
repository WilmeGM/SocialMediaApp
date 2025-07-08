using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<SavePostViewModel, PostViewModel, Post>
    {
        Task<List<PostViewModel>> GetAllWithCommentsAsync();
        Task<List<PostViewModel>> GetUserPostsAsync();
        Task<List<PostViewModel>> GetFriendsPostsAsync(string userId);
    }
}
