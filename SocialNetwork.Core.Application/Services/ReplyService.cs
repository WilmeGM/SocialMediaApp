using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Reply;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class ReplyService : GenericService<SaveReplyViewModel, ReplyViewModel, Reply>, IReplyService
    {
        private readonly IUserSessionService _userSessionService;

        public ReplyService(IReplyRepository replyRepository, IMapper mapper, IUserSessionService userSessionService)
            : base(replyRepository, mapper)
        {
            _userSessionService = userSessionService;
        }

        public override async Task<SaveReplyViewModel> AddAsync(SaveReplyViewModel vm)
        {
            vm.CreatedAt = DateTime.Now;
            vm.UserName = _userSessionService.GetUserName();
            vm.ProfilePictureUrl = _userSessionService.GetProfilePictureUrl();

            return await base.AddAsync(vm);
        }
    }
}
