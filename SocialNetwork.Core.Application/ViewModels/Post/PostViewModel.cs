using SocialNetwork.Core.Application.ViewModels.Comment;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string UserId { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
