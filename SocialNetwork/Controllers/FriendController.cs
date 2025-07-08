using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friendship;
using SocialNetwork.Core.Application.ViewModels.Reply;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IPostService _postService;
        private readonly IUserSessionService _userSessionService;
        private readonly ICommentService _commentService;
        private readonly IReplyService _replyService;

        public FriendController(
            IFriendshipService friendshipService,
            IPostService postService,
            IUserSessionService userSessionService,
            ICommentService commentService,
            IReplyService replyService)
        {
            _friendshipService = friendshipService;
            _postService = postService;
            _userSessionService = userSessionService;
            _commentService = commentService;
            _replyService = replyService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userSessionService.GetUserId();

            var friends = await _friendshipService.GetUserFriendsAsync(userId);
            var friendPosts = await _postService.GetFriendsPostsAsync(userId);

            var model = new FriendsViewModel
            {
                FriendsList = friends,
                FriendsPosts = friendPosts
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(string friendId)
        {
            var userId = _userSessionService.GetUserId();
            await _friendshipService.RemoveFriendAsync(userId, friendId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string friendUserName)
        {
            if (string.IsNullOrWhiteSpace(friendUserName))
            {
                TempData["ErrorMessage"] = "The username is required";
                return RedirectToAction("Index");
            }

            var userId = _userSessionService.GetUserId();
            string? result = await _friendshipService.AddFriendAsync(userId, friendUserName);

            if (result != null)
            {
                TempData["ErrorMessage"] = result;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(SaveCommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            vm.UserId = _userSessionService.GetUserId();
            await _commentService.AddAsync(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateReply(SaveReplyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            vm.UserId = _userSessionService.GetUserId();
            await _replyService.AddAsync(vm);
            return RedirectToAction("Index");
        }
    }
}
