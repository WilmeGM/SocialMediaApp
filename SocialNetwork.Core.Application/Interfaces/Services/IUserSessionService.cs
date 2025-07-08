namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserSessionService
    {
        bool IsLoggedIn();
        string GetUserName();
        string GetUserId();
        string GetProfilePictureUrl();
    }
}