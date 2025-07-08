namespace SocialNetwork.Core.Domain.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string UserId { get; set; } 
    }
}
