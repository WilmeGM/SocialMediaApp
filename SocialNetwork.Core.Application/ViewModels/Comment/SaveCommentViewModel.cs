using SocialNetwork.Core.Application.ViewModels.Reply;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This can not be empty.")]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int PostId { get; set; }
        public string? UserId { get; set; }
        public ICollection<ReplyViewModel>? Replies { get; set; }
    }
}
