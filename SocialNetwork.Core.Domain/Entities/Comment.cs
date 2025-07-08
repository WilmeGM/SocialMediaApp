namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string UserId { get; set; }
        public ICollection<Reply>? Replies { get; set; }
    }
}
