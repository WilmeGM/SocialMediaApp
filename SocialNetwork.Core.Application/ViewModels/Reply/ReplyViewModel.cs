using SocialNetwork.Core.Application.ViewModels.Comment;

namespace SocialNetwork.Core.Application.ViewModels.Reply
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } 
        public string? ProfilePictureUrl { get; set; }
        public int CommentId { get; set; }
        public CommentViewModel Comment { get; set; }
        public string UserId { get; set; }
    }
}
