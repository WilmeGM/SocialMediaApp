using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;
    
namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, PostViewModel, Post>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserSessionService _userSessionService;
        private readonly IFriendshipIdentityRepository _friendshipIdentityRepository;

        public PostService(IPostRepository postRepository, IMapper mapper, IUserSessionService userSessionService, IFriendshipIdentityRepository friendshipIdentityRepository) 
            : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userSessionService = userSessionService;
            _friendshipIdentityRepository = friendshipIdentityRepository;
        }

        public async Task<List<PostViewModel>> GetAllWithCommentsAsync()
        {
            var posts = await _postRepository.GetPostsWithCommentsAsync();
            return _mapper.Map<List<PostViewModel>>(posts);
        }

        public async Task<List<PostViewModel>> GetUserPostsAsync()
        {
            var posts = await _postRepository.GetPostsWithCommentsAsync();
            var userPosts = posts.Where(p => p.UserId == _userSessionService.GetUserId()).ToList();
            return _mapper.Map<List<PostViewModel>>(userPosts);
        }

        public override async Task<SavePostViewModel> AddAsync(SavePostViewModel vm)
        {
            vm.CreatedAt = DateTime.Now;
            vm.UserId = _userSessionService.GetUserId();
            vm.UserName = _userSessionService.GetUserName();
            vm.ProfilePictureUrl = _userSessionService.GetProfilePictureUrl();

            return await base.AddAsync(vm);
        }

        public async Task<List<PostViewModel>> GetFriendsPostsAsync(string userId)
        {
            var friends = await _friendshipIdentityRepository.GetUserFriendsAsync(userId);
            var friendUserIds = friends.Select(f => f.Id).ToList();

            var friendPosts = await _postRepository.GetPostsWithCommentsAsync();
            var friendPostsFiltered = friendPosts.Where(p => friendUserIds.Contains(p.UserId)).ToList();

            return _mapper.Map<List<PostViewModel>>(friendPostsFiltered);
        }
    }
}
