using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friendship;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipIdentityRepository _friendshipIdentityRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserSessionService _userSessionService;


        public FriendshipService(IFriendshipIdentityRepository friendshipIdentityRepository, IMapper mapper, IAccountService accountService, IUserSessionService userSessionService)
        {
            _friendshipIdentityRepository = friendshipIdentityRepository;
            _mapper = mapper;
            _accountService = accountService;
            _userSessionService = userSessionService;
        }

        public async Task<string> AddFriendAsync(string userId, string friendUsername)
        {
            if (friendUsername == _userSessionService.GetUserName())
            {
                return "It's you";
            }

            var friend = await _accountService.GetFriendByUsernameAsync(friendUsername);
            if (friend == null)
            {
                return "User not found";
            }

            var existingFriendship = await _friendshipIdentityRepository.GetFriendshipAsync(userId, friend.Id);
            if (existingFriendship != null)
            {
                return "Already friends";
            }

            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friend.Id
            };

            await _friendshipIdentityRepository.AddAsync(friendship);
            return null;
        }

        public async Task<List<FriendViewModel>> GetUserFriendsAsync(string userId)
        {
            var friends = await _friendshipIdentityRepository.GetUserFriendsAsync(userId);
            return _mapper.Map<List<FriendViewModel>>(friends);
        }

        public async Task RemoveFriendAsync(string userId, string friendId)
        {
            var friendship = await _friendshipIdentityRepository.GetFriendshipAsync(userId, friendId);
            if (friendship != null)
            {
                await _friendshipIdentityRepository.RemoveAsync(friendship);
            }
        }
    }
}
