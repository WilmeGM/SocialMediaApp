namespace SocialNetwork.Core.Application.Dtos.User
{
    public class RegisterResponse
    {
        public string? Id { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
