using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "You must enter your username.")]
        public string UserName { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
