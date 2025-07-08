using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Core.Application.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            return GetSessionUser() != null;
        }

        public string GetUserName()
        {
            var user = GetSessionUser();
            return user != null ? user.UserName : string.Empty;
        }

        public string GetUserId()
        {
            var user = GetSessionUser();
            return user != null ? user.Id : string.Empty;
        }

        public string GetProfilePictureUrl()
        {
            var user = GetSessionUser();
            return user != null ? user.ProfilePictureUrl : string.Empty;
        }

        private AuthenticationResponse GetSessionUser()
        {
            return _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
    }
}
