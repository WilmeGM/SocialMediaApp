using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.Reply;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.Helpers;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IReplyService _replyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;

        public HomeController(
            IPostService postService,
            ICommentService commentService,
            IReplyService replyService,
            IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _commentService = commentService;
            _replyService = replyService;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetUserPostsAsync();
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            if (vm.Image != null)
            {
                vm.ImageUrl = UploadFile(vm.Image, vm.Id);
            }

            await _postService.AddAsync(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditPost(int postId)
        {
            return View(await _postService.GetByIdSaveViewModelAsync(postId));
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(SavePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            await _postService.UpdateAsync(vm, vm.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeleteAsync(postId);

            string basePath = $"/Images/Posts/{postId}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                Directory.Delete(path);
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

            vm.UserId = _userViewModel.Id;
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

            vm.UserId = _userViewModel.Id;
            await _replyService.AddAsync(vm);
            return RedirectToAction("Index");
        }

        private string UploadFile(IFormFile file, int id)
        {
            string basePath = $"/Images/Posts/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{basePath}/{fileName}";
        }
    }
}
