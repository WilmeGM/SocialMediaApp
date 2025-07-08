namespace SocialNetwork.Core.Domain.Entities
{
    public class Friendship
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
