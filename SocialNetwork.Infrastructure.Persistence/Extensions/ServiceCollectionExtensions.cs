using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using SocialNetwork.Infrastructure.Persistence.Repositories;

namespace SocialNetwork.Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database connection configuration
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("Connection");
                services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString, migrations =>
                migrations.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IReplyRepository, ReplyRepository>();
            #endregion
        }
    }
}
