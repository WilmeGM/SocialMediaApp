namespace SocialNetwork.Core.Application.Dtos.User
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
