namespace SocialNetwork.Core.Application.Dtos.User
{
    public class ForgotPasswordResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
