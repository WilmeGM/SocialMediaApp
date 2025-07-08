using Microsoft.AspNetCore.Identity;
using SocialNetwork.Infrastructure.Identity.Entities;

namespace SocialNetwork.Infrastructure.Identity.Seeds
{
    public class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "basicuser",
                Email = "basicuser@example.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "809-123-4567",
                EmailConfirmed = true,  
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "User123!");
            }
        }
    }
}
