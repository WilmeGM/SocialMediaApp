using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Reply
{
    public class SaveReplyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This can not be empty.")]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int CommentId { get; set; }
        public string? UserId { get; set; }
    }
}
