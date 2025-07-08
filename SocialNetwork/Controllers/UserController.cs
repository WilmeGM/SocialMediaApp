using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Middlewares;
using SocialNetwork.Core.Application.Helpers;

namespace SocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }

        [ServiceFilter(typeof(LoginVerificator))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginVerificator))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginVerificator))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginVerificator))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            if (vm.ProfilePicture != null)
            {
                vm.ProfilePictureUrl = UploadFile(vm.ProfilePicture, response.Id);
                await _userService.UpdateUserProfilePictureAsync(response.Id, vm.ProfilePictureUrl);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        private string UploadFile(IFormFile file, string id)
        {
            string basePath = $"/Images/ProfilePictures/{id}";
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

        [ServiceFilter(typeof(LoginVerificator))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginVerificator))]
        public IActionResult ForgotPassword()
        {   
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginVerificator))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            ForgotPasswordResponse forgotPasswordResponse = await _userService.ForgotPasswordAsync(vm);
            if (forgotPasswordResponse.HasError)
            {
                vm.HasError = forgotPasswordResponse.HasError;
                vm.Error = forgotPasswordResponse.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
