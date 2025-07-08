using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetPostsWithCommentsAsync()
        {
            return await _dbContext.Posts
                                   .Include(post => post.Comments)
                                   .ThenInclude(comment => comment.Replies)
                                   .OrderByDescending(post => post.CreatedAt)
                                   .ToListAsync();
        }

        public async Task<List<Post>> GetFriendsPostsAsync(string userId, List<string> friendsIds)
        {
            return await _dbContext.Posts
                .Where(post => friendsIds.Contains(post.UserId))
                .Include(post => post.Comments)
                .ThenInclude(comment => comment.Replies)
                .OrderByDescending(post => post.CreatedAt)
                .ToListAsync();
        }
    }
}
