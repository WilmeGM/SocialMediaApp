using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using System.Reflection;

namespace SocialNetwork.Core.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserSessionService, UserSessionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IReplyService, ReplyService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
        }
    }
}
