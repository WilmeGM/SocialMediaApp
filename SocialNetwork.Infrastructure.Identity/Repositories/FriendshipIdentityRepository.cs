using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Dtos.Friendship;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using SocialNetwork.Infrastructure.Persistence.Repositories;

namespace SocialNetwork.Infrastructure.Identity.Repositories
{
    public class FriendshipIdentityRepository :  GenericRepository<Friendship>, IFriendshipIdentityRepository
    {
        private readonly IdentityContext _identityContext;
        private readonly ApplicationDbContext _applicationDbContext;

        public FriendshipIdentityRepository(IdentityContext identityContext, ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _identityContext = identityContext;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Friend>> GetUserFriendsAsync(string userId)
        {
            var friendIds = await _applicationDbContext.Friendships
            .Where(f => f.UserId == userId)
            .Select(f => f.FriendId)
            .ToListAsync();

            return await _identityContext.Users
                .Where(u => friendIds.Contains(u.Id))
                .Select(user => new Friend
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    ProfilePictureUrl = user.ProfilePictureUrl
                })
                .ToListAsync();
        }

        public async Task<Friendship> GetFriendshipAsync(string userId, string friendId)
        {
            return await _applicationDbContext.Friendships
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
        }
    }
}
