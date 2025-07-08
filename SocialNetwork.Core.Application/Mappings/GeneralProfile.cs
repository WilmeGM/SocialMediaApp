using AutoMapper;
using SocialNetwork.Core.Application.Dtos.Friendship;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friendship;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.Reply;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region User
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Post
            CreateMap<Post, PostViewModel>()
                .ReverseMap();

            CreateMap<Post, SavePostViewModel>()
                .ForMember(svm => svm.Image, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Comment
            CreateMap<Comment, CommentViewModel>()
                .ReverseMap();

            CreateMap<Comment, SaveCommentViewModel>()
                .ReverseMap()
                .ForMember(c => c.Post, opt => opt.Ignore());
            #endregion

            #region Reply
            CreateMap<Reply, ReplyViewModel>()
                .ReverseMap();

            CreateMap<Reply, SaveReplyViewModel>()
                .ReverseMap()
                .ForMember(r => r.Comment, opt => opt.Ignore());
            #endregion

            #region Friendship
            CreateMap<Friend, FriendViewModel>()
                .ReverseMap();
            #endregion
        }
    }   
}
