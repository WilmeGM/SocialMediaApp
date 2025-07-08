using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.ViewModels.Comment;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "This can not be empty.")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }

        [DataType(DataType.Text)]
        public string? YoutubeUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public ICollection<CommentViewModel>? Comments { get; set; }
    }
}
