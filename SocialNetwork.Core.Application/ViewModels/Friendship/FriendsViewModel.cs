using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Core.Application.ViewModels.Friendship
{
    public class FriendsViewModel
    {
        public List<PostViewModel> FriendsPosts { get; set; }
        public List<FriendViewModel> FriendsList { get; set; }
    }
}
