using Microsoft.AspNetCore.Mvc.Filters;
using SocialNetwork.Controllers;
using SocialNetwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Middlewares
{
    public class LoginVerificator : IAsyncActionFilter
    {
        private readonly IUserSessionService _userSessionService;

        public LoginVerificator(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSessionService.IsLoggedIn())
            {
                var controller = (UserController)context.Controller;
                context.Result = controller.RedirectToAction("index", "home");
            }
            else
            {
                await next();
            }
        }
    }
}
