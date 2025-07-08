using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Reply>().ToTable("Replies");
            modelBuilder.Entity<Friendship>().ToTable("Friendships");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Post>().HasKey(post => post.Id);
            modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
            modelBuilder.Entity<Reply>().HasKey(reply => reply.Id);
            modelBuilder.Entity<Friendship>().HasKey(f => new { f.UserId, f.FriendId });
            #endregion

            #region Relationships
            // Post - Comments (1:N)
            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(comment => comment.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment - Replies (1:N)
            modelBuilder.Entity<Comment>()
                .HasMany(comment => comment.Replies)
                .WithOne(reply => reply.Comment)
                .HasForeignKey(reply => reply.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Property Configurations
            // Post
            modelBuilder.Entity<Post>()
                .Property(post => post.Content)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(post => post.CreatedAt)
                .IsRequired();

            // Comment
            modelBuilder.Entity<Comment>()
                .Property(comment => comment.Content)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(comment => comment.CreatedAt)
                .IsRequired();

            // Reply
            modelBuilder.Entity<Reply>()
                .Property(reply => reply.Content)
                .IsRequired();

            modelBuilder.Entity<Reply>()
                .Property(reply => reply.CreatedAt)
                .IsRequired();

            #endregion
        }
    }
}
