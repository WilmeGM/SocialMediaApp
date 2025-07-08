using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
