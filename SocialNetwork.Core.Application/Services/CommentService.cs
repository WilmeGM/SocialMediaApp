using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly IUserSessionService _userSessionService;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IUserSessionService userSessionService)
            : base(commentRepository, mapper)
        {
            _userSessionService = userSessionService;
        }

        public override async Task<SaveCommentViewModel> AddAsync(SaveCommentViewModel vm)
        {
            vm.CreatedAt = DateTime.Now;
            vm.UserName = _userSessionService.GetUserName();
            vm.ProfilePictureUrl = _userSessionService.GetProfilePictureUrl();

            return await base.AddAsync(vm);
        }
    }
}
