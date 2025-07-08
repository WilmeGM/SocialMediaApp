using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IUserService _userService;

        public ProfileController(IUserSessionService userSessionService, IUserService userService)
        {
            _userSessionService = userSessionService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userSessionService.GetUserId();
            var userSaveViewModel = await _userService.GetSaveUserViewModelByIdAsync(userId);

            return View(userSaveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }

            try
            {
                var existingUser = await _userService.GetSaveUserViewModelByIdAsync(vm.Id);
                vm.ProfilePictureUrl = UploadFile(vm.ProfilePicture, vm.Id, true, existingUser.ProfilePictureUrl);
                await _userService.UpdateUserProfileAsync(vm);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index", vm);
            }
        }

        private string UploadFile(IFormFile file, string id, bool editMode = false, string imageUrl = "")
        {
            if (editMode && file == null) 
            { 
                return imageUrl; 
            }

            var guid = Guid.NewGuid();
            var fileInfo = new FileInfo(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string basePath = $"/Images/ProfilePictures/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            string pathWithFileName = Path.Combine(path, fileName);

            if (!Directory.Exists(path)) 
            { 
                Directory.CreateDirectory(path); 
            }

            using (var stream = new FileStream(pathWithFileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (editMode)
            {
                string[] oldImagePart = imageUrl.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{fileName}";
        }
    }
}
