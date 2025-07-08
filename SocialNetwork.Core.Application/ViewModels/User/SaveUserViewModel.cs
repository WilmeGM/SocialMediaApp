using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePictureUrl { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords must be the same.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
