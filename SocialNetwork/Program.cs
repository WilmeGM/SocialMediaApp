using Microsoft.AspNetCore.Identity;
using SocialNetwork.Infrastructure.Identity.Entities;
using SocialNetwork.Infrastructure.Identity.Extensions;
using SocialNetwork.Infrastructure.Shared.Extensions;
using SocialNetwork.Infrastructure.Identity.Seeds;
using SocialNetwork.Core.Application.Extensions;
using SocialNetwork.Middlewares;
using SocialNetwork.Infrastructure.Persistence.Extensions;

namespace SocialNetwork
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddSession();
            builder.Services.AddPersistenceInfrastructure(builder.Configuration);
            builder.Services.AddApplicationCore();
            builder.Services.AddIdentityInfrastructure(builder.Configuration);
            builder.Services.AddSharedInfrastructure(builder.Configuration);
            builder.Services.AddScoped<LoginVerificator>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    await SeedUsers(scope.ServiceProvider);
                }
                catch (Exception ex) {}
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Index}/{id?}");
            app.Run();
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await DefaultBasicUser.SeedAsync(userManager);
        }
    }
}